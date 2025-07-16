import { ValidatorFn, Validators } from '@angular/forms';

export interface FieldConfig {
  label: string;
  name: string;
  type: string;
  validators?: ValidatorFn[];
}

export const generalFields: FieldConfig[] = [
  { label: 'Account Number', name: 'accountNumber', type: 'text', validators: [Validators.required] },
  { label: 'Company Name', name: 'companyName', type: 'text', validators: [Validators.required] },
  { label: 'Phone', name: 'phone', type: 'text', validators: [Validators.required, Validators.pattern(/^\d{11}$/)]},
  { label: 'Mobile', name: 'mobile', type: 'text', validators: [Validators.required, Validators.pattern(/^\d{11}$/)]},
  { label: 'Contact', name: 'contact', type: 'text', validators: [Validators.required] },
  { label: 'Trading Name', name: 'tradingName', type: 'text' }
];

export const loginFields: FieldConfig[] = [
  { label: 'Email', name: 'email', type: 'loginEmail', validators: [Validators.required, Validators.email] },
  { label: 'Password', name: 'password', type: 'password' }
];

export const accountFields: FieldConfig[] = [
  { label: 'Name', name: 'name', type: 'text', validators: [Validators.required] },
  { label: 'Email', name: 'accountEmail', type: 'email', validators: [Validators.required, Validators.email] },
  { label: 'Email (Bill)', name: 'emailBill', type: 'email', validators: [Validators.required, Validators.email] },
  { label: 'Phone', name: 'accountPhone', type: 'text', validators: [Validators.required, Validators.pattern(/^\d{11}$/)]},
  { label: 'Company Name', name: 'companyName', type: 'text', validators: [Validators.required] },
  { label: 'Account Manager', name: 'accountManager', type: 'text', validators: [Validators.required] },
];

export const deliveryFields: FieldConfig[] = [
  { label: 'Bill To', name: 'billTo', type: 'text' },
  { label: 'Ship To', name: 'shipTo', type: 'text' },
  { label: 'Street', name: 'street', type: 'text' },
  { label: 'City', name: 'city', type: 'text' },
  { label: 'State', name: 'state', type: 'text' },
  { label: 'ZIP', name: 'zip', type: 'text' },
  { label: 'Country', name: 'country', type: 'text' }
];

export const contractOptions = [
    { value: '36', label: '36' },
    { value: 'open', label: 'Open' }
  ];
  
export const checkboxGroups = [
  {
    label: 'Options',
    options: [
      { label: 'Active Account', controlName: 'activeAccount' },
      { label: 'Master Account', controlName: 'masterAccount' },
      { label: 'Biling CSV', controlName: 'billingCsv' },
      { label: 'ejobs Client', controlName: 'ejobsClient' },
      { label: 'GOG', controlName: 'gog' }
    ]
  },
  {
    label: 'Plug-ins',
    options: [
      { label: 'Smart Renew', controlName: 'smartRenew' },
      { label: 'Custom Branding', controlName: 'customBranding' },
      { label: 'Send Message', controlName: 'sendMessage' }
    ]
  },
  {
    label: 'Integrations',
    options: [
      { label: 'SOS Event Push', controlName: 'sosEventPush' },
      { label: 'PVBS Client', controlName: 'pvbsClient' },
      { label: 'vWork Client', controlName: 'vworkClient' },
      { label: 'SSO', controlName: 'sso' },
      { label: 'API Key', controlName: 'apiKey' }
    ]
  }
];

export const planTypes = [
  {name: 'roadRedPlan', label: 'Default Road Red Plan'},
  {name: 'iotPlan', label: 'Default Other (IoT) Plan'}, 
  {name: 'softwarePlan', label: 'Default Software Plan'}
]

export const planOptions = ['VISI', 'MIDI', 'OMNI', 'ASSIST', 'LOCI']

export const contractCheckboxes = [
  { name: 'assignPPToAllAVLs', label: 'Assign PP to all AVLs'},
  { name: 'rolloverAgreement', label: 'Rollover Agreement'},
  { name: 'nonBillingAccount', label: 'Non Billing Account'},
]