import { ValidatorFn, Validators } from '@angular/forms';

export interface FieldConfig {
  name: string;
  value?: any;
  disabled?: boolean;
  validators?: ValidatorFn[];
  updateOn?: 'change' | 'blur' | 'submit';
}

export const fieldConfigs: FieldConfig[] = [
  { name: 'accountNumber', value: '', disabled: true, updateOn: 'blur' },
    { name: 'companyName', value: '', validators: [Validators.required] },
    { name: 'phone', value: '', validators: [Validators.required, Validators.pattern(/^\d{11}$/)], updateOn: 'blur' },
    { name: 'mobile', value: '', validators: [Validators.required, Validators.pattern(/^\d{11}$/)], updateOn: 'blur' },
    { name: 'contact', value: '', validators: [Validators.required], updateOn: 'blur' },
    { name: 'tradingName', value: '', updateOn: 'blur' },
    { name: 'loginEmail', value: '', validators: [Validators.required, Validators.email], updateOn: 'blur' },
    { name: 'generatedPassword', value: '' },
    { name: 'accountName', value: '', validators: [Validators.required], updateOn: 'blur' },
    { name: 'accountEmail', value: '', validators: [Validators.required, Validators.email], updateOn: 'blur' },
    { name: 'accountEmailBill', value: '', validators: [Validators.required, Validators.email], updateOn: 'blur' },
    { name: 'accountPhone', value: '', validators: [Validators.required, Validators.pattern(/^\d{11}$/)], updateOn: 'blur' },
    { name: 'accountCompanyName', value: '', validators: [Validators.required], updateOn: 'blur' },
    { name: 'accountManager', value: '', validators: [Validators.required], updateOn: 'blur' },
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
    { name: 'contractTerm', value: '36', validators: [Validators.required] },
    { name: 'customValue', value: 36 },
    { name: 'roadRedPlan', value: '' },
    { name: 'iotPlan', value: '' },
    { name: 'softwarePlan', value: '' },
    { name: 'assignPPToAllAVLs', value: false },
    { name: 'rolloverAgreement', value: false },
    { name: 'nonBillingAccount', value: false },
];
