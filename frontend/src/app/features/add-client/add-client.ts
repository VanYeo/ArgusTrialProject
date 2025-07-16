import {
  Component,
  ElementRef,
  EventEmitter,
  HostListener,
  inject,
  Input,
  OnInit,
  Output,
  QueryList,
  ViewChildren,
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
  @Output() closed = new EventEmitter<void>();
  @Input() isEdit: boolean = false;

  form: FormGroup;
  hidePassword = true;
  isEditMode = false;

  // Dropdowns
  showDropdowns: boolean[] = new Array(planTypes.length).fill(false);

  // Field/option constants
  checkboxGroups = checkboxGroups;
  contractOptions = contractOptions;
  planTypes = planTypes;
  planOptions = planOptions;
  contractCheckboxes = contractCheckboxes;

  // Focus states
  phoneFocused = false;
  mobileFocused = false;
  accountPhoneFocused = false;

  private addClientService = inject(AddClientService);
  private viewClientService = inject(ViewClientService);
  private route = inject(ActivatedRoute);
  private eRef = inject(ElementRef);
  client: any;

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

  private router = inject(Router);
  ngOnInit(): void {
    console.log('🔥 AddClientComponent ngOnInit fired');
    const currentUrl = this.router.url;

    this.isEditMode = currentUrl.includes('/clients/edit');
    console.log(this.isEditMode);

    if (this.isEditMode) {
      const idParam = this.route.snapshot.paramMap.get('id');

      if (!idParam || isNaN(Number(idParam))) {
        console.error('Invalid or missing client ID in route.');
        return;
      }

      const clientId = Number(idParam);
      console.log('[AddClientComponent] Edit Mode for ID:', clientId);

      this.viewClientService.getClientById(clientId).subscribe({
        next: (client) => {
          this.client = client;
          console.log('[AddClientComponent] Client data loaded:', client);
          this.form.patchValue(client);
          if (client.contractTerm) {
            this.form.get('contractTerm')?.setValue(client.contractTerm);
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
      console.log('[AddClientComponent] Create Mode');

      this.addClientService.getNewAccountNumber().subscribe({
        next: (newAccountNumber: string) => {
          this.form.get('accountNumber')?.setValue(newAccountNumber);
        },
        error: (err) => {
          console.error('Failed to fetch new account number:', err);
        },
      });
    }

    // Auto-fill account company name
    this.form.get('companyName')?.valueChanges.subscribe((value) => {
      const altControl = this.form.get('accountCompanyName');
      if (value && !altControl?.value) {
        altControl?.setValue(value);
      }
    });

    // Auto-generate password if creating new client
    this.form.get('loginEmail')?.valueChanges.subscribe(() => {
      const emailControl = this.form.get('loginEmail');
      if (emailControl?.valid && !this.isEditMode) {
        const password = this.generateRandomPassword();
        this.form.get('generatedPassword')?.setValue(password);
      }
    });
  }

  onFieldFocus(controlName: string) {
    const control = this.form.get(controlName);
    console.log(
      `[Focus] ${controlName} | touched: ${control?.touched}, dirty: ${control?.dirty}, status: ${control?.status}`
    );
  }

  // --- Field configs ---
  private fieldConfigs: FieldConfig[] = [
    { name: 'accountNumber', value: '', disabled: true, updateOn: 'blur' },
    {
      name: 'companyName',
      value: '',
      validators: [Validators.required],
    },
    {
      name: 'phone',
      value: '',
      validators: [Validators.required, Validators.pattern(/^\d{11}$/)],
      updateOn: 'blur',
    },
    {
      name: 'mobile',
      value: '',
      validators: [Validators.required, Validators.pattern(/^\d{11}$/)],
      updateOn: 'blur',
    },
    {
      name: 'contact',
      value: '',
      validators: [Validators.required],
      updateOn: 'blur',
    },
    { name: 'tradingName', value: '', updateOn: 'blur' },
    {
      name: 'loginEmail',
      value: '',
      validators: [Validators.required, Validators.email],
      updateOn: 'blur',
    },
    { name: 'generatedPassword', value: '' },
    {
      name: 'accountName',
      value: '',
      validators: [Validators.required],
      updateOn: 'blur',
    },
    {
      name: 'accountEmail',
      value: '',
      validators: [Validators.required, Validators.email],
      updateOn: 'blur',
    },
    {
      name: 'accountEmailBill',
      value: '',
      validators: [Validators.required, Validators.email],
      updateOn: 'blur',
    },
    {
      name: 'accountPhone',
      value: '',
      validators: [Validators.required, Validators.pattern(/^\d{11}$/)],
      updateOn: 'blur',
    },
    {
      name: 'accountCompanyName',
      value: '',
      validators: [Validators.required],
      updateOn: 'blur',
    },
    {
      name: 'accountManager',
      value: '',
      validators: [Validators.required],
      updateOn: 'blur',
    },
    { name: 'notes', value: '', updateOn: 'blur' },
    { name: 'activeAccount', value: false },
    { name: 'masterAccount', value: false },
    { name: 'billingCsv', value: false },
    { name: 'ejobsClient', value: false },
    { name: 'gog', value: false },
    { name: 'smartRenew', value: false },
    { name: 'customBranding', value: false },
    { name: 'sendMessage', value: false },
    { name: 'sosEventPush', value: false },
    { name: 'pvbsClient', value: false },
    { name: 'vworkClient', value: false },
    { name: 'sso', value: false },
    { name: 'apiKey', value: false },
    { name: 'startDate', value: null, validators: [Validators.required] },
    { name: 'contractTerm', value: '', validators: [Validators.required] },
    { name: 'customValue', value: 36 },
    { name: 'roadRedPlan', value: '' },
    { name: 'iotPlan', value: '' },
    { name: 'softwarePlan', value: '' },
    { name: 'assignPPToAllAVLs', value: false },
    { name: 'rolloverAgreement', value: false },
    { name: 'nonBillingAccount', value: false },
  ];

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

  // Getters for nested forms
  get billing() {
    return this.form.get('billingAddress') as FormGroup;
  }
  get delivery() {
    return this.form.get('deliveryAddress') as FormGroup;
  }

  // Blur handlers
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

  shouldShowError(controlName: string): boolean {
    const control = this.form.get(controlName);
    return !!(control && control.invalid && (control.touched || control.dirty));
  }

  // Copy billing -> delivery
  copyFromBillingAddress() {
    this.delivery.patchValue(this.billing.value);
  }

  // Dropdown logic
  selectOption(controlName: string, index: number, option: string) {
    this.form.get(controlName)?.setValue(option);
    this.showDropdowns[index] = false;
  }

  toggleDropdown(index: number) {
    this.showDropdowns = this.showDropdowns.map((val, i) =>
      i === index ? !val : false
    );
  }

  @ViewChildren('dropdownGroup') dropdownGroups!: QueryList<ElementRef>;

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

  closeForm() {
    this.closed.emit();
  }

  // navigate to view client details from edit form
  navigateToViewClient() {
    const clientId = this.form.get('clientID')?.value;
    if (clientId) {
      this.router.navigate(['/clients/', clientId]);
    } else {
      this.router.navigate(['/clients']);
    }
  }

  handleCancel() {
    if (this.isEditMode) {
      this.navigateToViewClient();
    } else {
      this.router.navigate(['/clients']);
    }
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

      if (this.isEditMode) {
        console.log('Payload:', clientPayload);
        this.addClientService.updateClient(clientPayload).subscribe({
          next: (res) => {
            console.log('✅ Client updated:', res);
            this.closed.emit();
            const clientId = this.form.get('clientID')?.value;
            this.router.navigate(['/clients', clientId]); // Redirect to view client
          },
          error: (err) => console.error('❌ Error updating client:', err),
        });
      } else {
        this.addClientService.addClient(clientPayload).subscribe({
          next: (res) => {
            console.log('✅ Client created:', res);
            this.closed.emit();
            this.router.navigate(['/clients']); // move this inside next
          },
          error: (err) => console.error('❌ Error creating client:', err),
        });
      }
    } else {
      console.log('Form is invalid');
      this.form.markAllAsTouched();
    }
  }
}
