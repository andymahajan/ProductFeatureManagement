import { AbstractControl, ValidatorFn } from '@angular/forms';

export class DateValidators {
  static futureDate: ValidatorFn = (control: AbstractControl): { [key: string]: boolean } | null => {
    const today = new Date();
    if (control.value && new Date(control.value) <= today) {
      return { 'futureDate': true };
    }
    return null;
  };
}
