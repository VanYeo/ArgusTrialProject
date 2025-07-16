import {
  Component,
  ElementRef,
  EventEmitter,
  HostListener,
  Input,
  OnInit,
  Output,
  QueryList,
  ViewChild,
  ViewChildren,
  inject,
} from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
  ValidatorFn,
} from '@angular/forms';
import {
  checkboxGroups,
  contractOptions,
  planOptions,
  planTypes,
  contractCheckboxes,
} from './add-client-fields';
import { AddClientService } from './add-client.service';
import { ViewClientService } from '../view-client/view-client.service';
import { ActivatedRoute, Router } from '@angular/router';
import { fieldConfigs } from './add-client-validators';

interface FieldConfig {
  name: string;
  value?: any;
  disabled?: boolean;
  validators?: ValidatorFn[];
  updateOn?: 'change' | 'blur' | 'submit';
}

@Component({
  selector: 'app-add-client',
  standalone: false,
  templateUrl: './add-client.html',
  styleUrl: './add-client.scss',
})
export class AddClientComponent implements OnInit {
  @Input() clientData: any;
  @Input() isEdit: boolean = false;
  @Output() closed = new EventEmitter<void>();

  @ViewChild('toast') toastComponent!: any;
  @ViewChildren('dropdownGroup') dropdownGroups!: QueryList<ElementRef>;

  form: FormGroup;
  hidePassword = true;
  isEditMode = false;

  checkboxGroups = checkboxGroups;
  contractOptions = contractOptions;
  planTypes = planTypes;
  planOptions = planOptions;
  contractCheckboxes = contractCheckboxes;
  fieldConfigs = fieldConfigs;

  showDropdowns: boolean[] = new Array(planTypes.length).fill(false);

  phoneFocused = false;
  mobileFocused = false;
  accountPhoneFocused = false;

  private addClientService = inject(AddClientService);
  private viewClientService = inject(ViewClientService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  client: any;

  // build form
  private buildControls(configs: FieldConfig[]) {
    const group: { [key: string]: FormControl } = {};
    configs.forEach((cfg) => {
      group[cfg.name] = new FormControl(
        { value: cfg.value, disabled: cfg.disabled || false },
        {
          validators: cfg.validators || [],
          updateOn: cfg.updateOn || 'blur',
        }
      );
    });
    return group;
  }
  constructor(private fb: FormBuilder) {
    this.form = this.fb.group(
      {
        ...this.buildControls(this.fieldConfigs),
        billingAddress: this.fb.group(
          {
            street: ['', Validators.required],
            city: ['', Validators.required],
            state: ['', Validators.required],
            zip: ['', [Validators.required, Validators.pattern(/^\d{4}$/)]],
            country: ['', Validators.required],
          },
          { updateOn: 'blur' }
        ),
        deliveryAddress: this.fb.group({
          street: ['', Validators.required],
          city: ['', Validators.required],
          state: ['', Validators.required],
          zip: ['', [Validators.required, Validators.pattern(/^\d{4}$/)]],
          country: ['', Validators.required],
        }),
        clientID: [''],
        connections: [0],
      },
      { updateOn: 'blur' }
    );
  }

  ngOnInit(): void {
    // determining if add or edit client
    this.isEditMode = this.router.url.includes('/clients/edit');

    if (this.isEditMode) {
      const idParam = this.route.snapshot.paramMap.get('id');
      if (!idParam || isNaN(Number(idParam))) {
        console.error('Invalid or missing client ID in route.');
        return;
      }

      const clientId = Number(idParam);
      this.viewClientService.getClientById(clientId).subscribe({
        next: (client) => {
          this.client = client;
          if (client.contractTerm === 'custom') {
            client.customValue = Number(client.customValue);
          }

          this.form.patchValue(client);
          const contractTermValue = client.contractTerm?.toLowerCase();

          if (contractTermValue === 'custom' || contractTermValue === '') {
            this.form.get('contractTerm')?.setValue('custom');
            this.form.get('customValue')?.setValue(client.customValue ?? null);
          } else {
            this.form
              .get('contractTerm')
              ?.setValue(contractTermValue ?? 'open');
          }

          if (client.billingAddress) {
            this.billing.patchValue(client.billingAddress);
          }
          if (client.deliveryAddress) {
            this.delivery.patchValue(client.deliveryAddress);
          }
        },
        error: (err) => {
          console.error('Failed to load client for edit:', err);
        },
      });
    } else {
      this.addClientService.getNewAccountNumber().subscribe({
        next: (newAccountNumber: string) => {
          this.form.get('accountNumber')?.setValue(newAccountNumber);
        },
        error: (err) => {
          console.error('Failed to fetch new account number:', err);
        },
      });
    }

    // autofill companyname
    this.form.get('companyName')?.valueChanges.subscribe((value) => {
      const altControl = this.form.get('accountCompanyName');
      if (value && !altControl?.value) {
        altControl?.setValue(value);
      }
    });

    // autofill generated password
    this.form.get('loginEmail')?.valueChanges.subscribe(() => {
      const emailControl = this.form.get('loginEmail');
      if (emailControl?.valid && !this.isEditMode) {
        const password = this.generateRandomPassword();
        this.form.get('generatedPassword')?.setValue(password);
      }
    });
  }

  get billing() {
    return this.form.get('billingAddress') as FormGroup;
  }

  get delivery() {
    return this.form.get('deliveryAddress') as FormGroup;
  }

  onPhoneBlur() {
    this.phoneFocused = false;
    this.form.get('phone')?.markAsTouched();
  }

  onMobileBlur() {
    this.mobileFocused = false;
    this.form.get('mobile')?.markAsTouched();
  }

  onAccountPhoneBlur() {
    this.accountPhoneFocused = false;
    this.form.get('accountPhone')?.markAsTouched();
  }

  copyFromBillingAddress() {
    this.delivery.patchValue(this.billing.value);
  }

  selectOption(controlName: string, index: number, option: string) {
    this.form.get(controlName)?.setValue(option);
    this.showDropdowns[index] = false;
  }

  toggleDropdown(index: number) {
    this.showDropdowns = this.showDropdowns.map((val, i) =>
      i === index ? !val : false
    );
  }

  @HostListener('document:click', ['$event'])
  onClickOutside(event: MouseEvent) {
    const clickedInsideAny = this.dropdownGroups?.some((group) =>
      group.nativeElement.contains(event.target)
    );

    if (!clickedInsideAny) {
      this.showDropdowns = this.showDropdowns.map(() => false);
    }
  }

  generateRandomPassword(length: number = 12): string {
    const chars =
      'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@#?!$';
    return Array.from(
      { length },
      () => chars[Math.floor(Math.random() * chars.length)]
    ).join('');
  }

  navigateToViewClient() {
    const clientId = this.form.get('clientID')?.value;
    this.router.navigate(['/clients', clientId || '']);
  }

  handleCancel() {
    this.navigateToViewClient();
  }

  onSubmit(): void {
    if (!this.form.contains('clientID')) {
      this.form.addControl(
        'clientID',
        this.fb.control(this.form.get('accountNumber')?.value)
      );
    }

    if (!this.isEditMode) {
      this.form
        .get('clientID')
        ?.setValue(this.form.get('accountNumber')?.value);
      this.form.get('connections')?.setValue(0);
    }

    if (this.form.valid) {
      const clientPayload = this.form.value;

      const request = this.isEditMode
        ? this.addClientService.updateClient(clientPayload)
        : this.addClientService.addClient(clientPayload);

      request.subscribe({
        next: () => {
          const message = this.isEditMode
            ? 'Client updated successfully!'
            : 'Client added successfully!';
          this.toastComponent.showToast(message, 'success');
          this.closed.emit();
          setTimeout(() => {
            const clientId = this.form.get('clientID')?.value;
            this.router.navigate(['/clients', clientId || '']);
          }, 1000);
        },
        error: (err) => {
          const message = this.isEditMode
            ? 'Failed to update client.'
            : 'Error creating client';
          console.error(message, err);
          this.toastComponent.showToast(message, 'error');
        },
      });
    } else {
      console.log('Form is invalid');
      this.form.markAllAsTouched();
      this.toastComponent.showToast('Form is invalid', 'error');
    }
  }
}
