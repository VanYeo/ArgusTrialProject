export interface Address {
  street: string;
  city: string;
  state: string;
  zip: string;
  country: string;
}

export interface Client {
  clientID: number;
  accountNumber: number;
  companyName: string;
  tradingName: string;
  contact: string;
  phone: string;
  mobile: string;
  connections: number;
  loginEmail: string;
  generatedPassword: string;
  accountName: string;
  accountEmail: string;
  accountEmailBill: string;
  accountPhone: string;
  accountCompanyName: string;
  accountManager: string;
  billingAddress: Address;
  deliveryAddress: Address;
  activeAccount: boolean;
  masterAccount: boolean;
  billingCsv: boolean;
  ejobsClient: boolean;
  gog: boolean;
  nonBillingAccount: boolean;
  pvbsClient: boolean;
  vworkClient: boolean;
  sso: boolean;
  apiKey: boolean;
  assignPPToAllAVLs: boolean;
  sendMessage: boolean;
  sosEventPush: boolean;
  smartRenew: boolean;
  customBranding: boolean;
  workClient: boolean;
  rolloverAgreement: boolean;
  roadRedPlan: 'VISI' | 'MIDI' | 'OMNI' | 'ASSIST' | 'LOCI';
  iotPlan: 'VISI' | 'MIDI' | 'OMNI' | 'ASSIST' | 'LOCI';
  softwarePlan: 'VISI' | 'MIDI' | 'OMNI' | 'ASSIST' | 'LOCI';
  contractTerm: string;
  customValue: number;
  notes: string;
  startDate: string;
  [key: string]: any;
}

export const clientDetails: {
  header: string;
  fields: { name: keyof Client; label: string }[];
}[] = [
  {
    header: 'General Information',
    fields: [
      { name: 'clientID', label: 'Client ID' },
      { name: 'accountNumber', label: 'Account Number' },
      { name: 'companyName', label: 'Company Name' },
      { name: 'contact', label: 'Contact' },
      { name: 'phone', label: 'Phone' },
      { name: 'mobile', label: 'Mobile' },
      { name: 'tradingName', label: 'Trading Name' },
      { name: 'connections', label: 'Connections' },
    ],
  },
  {
    header: 'Login',
    fields: [
      { name: 'loginEmail', label: 'Email' },
      { name: 'generatedPassword', label: 'Password' },
    ],
  },
  {
    header: 'Account',
    fields: [
      { name: 'accountName', label: 'Account Name' },
      { name: 'accountEmail', label: 'Account Email' },
      { name: 'accountEmailBill', label: 'Account Email (Billing)' },
      { name: 'accountPhone', label: 'Account Phone' },
      { name: 'companyName', label: 'Company Name' },
      { name: 'accountManager', label: 'Account Manager' },
    ],
  },
];

export const AddressFields: { name: keyof Address; label: string }[] = [
  { name: 'street', label: 'Street' },
  { name: 'city', label: 'City' },
  { name: 'state', label: 'State' },
  { name: 'zip', label: 'Zip' },
  { name: 'country', label: 'Country' },
];

export const ExtraOptions: {
  header: string;
  fields: { name: keyof Client; label: string }[];
}[] = [
  {
    header: 'Options',
    fields: [
      { name: 'activeAccount', label: 'Active Account' },
      { name: 'masterAccount', label: 'Master Account' },
      { name: 'billingCSV', label: 'Billing CSV' },
      { name: 'ejobsClient', label: 'eJobs Client' },
      { name: 'gog', label: 'GOG' },
    ],
  },
  {
    header: 'Plug-ins',
    fields: [
      { name: 'smartRenew', label: 'Smart Renew' },
      { name: 'customBranding', label: 'Custom Branding' },
      { name: 'sendMessage', label: 'Send Message' },
    ],
  },
  {
    header: 'Integrations',
    fields: [
      { name: 'sosEventPush', label: 'SOS Event Push' },
      { name: 'pvbsClient', label: 'PVBS Client' },
      { name: 'vWorkClient', label: 'vWork Client' },
      { name: 'sso', label: ' SSO' },
      { name: 'apiKey', label: 'API Key' },
    ],
  }
];


export const ContractCheckboxes = [
  { name: 'assignPPToAllAVLs', label: 'Assign PP to all AVLs' },
  { name: 'rolloverAgreement', label: 'Rollover Agreement' },
  { name: 'nonBillingAccount', label: 'Non Billing Account' }
]

export const ContractPlans = [
  { name: 'roadRedPlan', label: 'Default Road Red Plan' },
  { name: 'iotPlan', label: 'Default Other (IoT) Plan' },
  { name: 'softwarePlan', label: 'Default Software Plan' }
]