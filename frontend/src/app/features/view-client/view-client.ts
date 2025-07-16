import { Component, inject } from '@angular/core';
import {
  AddressFields,
  Client,
  clientDetails,
  ContractCheckboxes,
  ContractPlans,
  ExtraOptions,
} from './client-model';
import { ActivatedRoute, Router } from '@angular/router';
import { ViewClientService } from './view-client.service';
import { calculateContractExpiry } from '../../service/contract-end-calculator';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-view-client',
  standalone: false,
  templateUrl: './view-client.html',
  styleUrl: './view-client.scss',
})
export class ViewClient {
  client!: Client;
  private route = inject(ActivatedRoute);
  private viewClientService = inject(ViewClientService);
  private router = inject(Router);

  clientDetails = clientDetails;
  addresses = AddressFields;
  extraOptions = ExtraOptions;
  contractCheckboxes = ContractCheckboxes;
  contractPlans = ContractPlans;

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.viewClientService.getClientById(id).subscribe((client) => {
      this.client = client;
    });
  }

  onCalculateContractExpiry(
    startDate: string,
    contractTerm: string,
    customValue: number
  ) {
    return calculateContractExpiry(startDate, contractTerm, customValue);
  }

  form = new FormGroup({
    accountEmail: new FormControl('', [Validators.required, Validators.email]),
  });

  editClient() {
  console.log('[ViewClientComponent] Attempting to navigate to edit route...');
  console.log('[ViewClientComponent] Client to edit:', this.client);

  this.router.navigate(['/clients/edit'], {
    state: { client: this.client }
  }).then(success => {
    console.log('[ViewClientComponent] Navigation success:', success);
  }).catch(error => {
    console.error('[ViewClientComponent] Navigation error:', error);
  });
}
}
