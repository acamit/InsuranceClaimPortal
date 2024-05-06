export interface Policy {
  id: number;
  policyName: string;
  policyNumber: number;
  policyEffectiveDate: string;
  policyExpirationDate: string;
  paymentOption: string;
  totalAmount: number;
  active: true;
  additionalInfo: string;
  createdDate: string;
  vehicle: null;
  drivers: null;
  policyCoverages: null;
}
