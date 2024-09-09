import { Component, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FeatureService } from '../../../services/feature.service';
import { Feature } from '../../../models/feature.model';
import { ComplexityService } from '../../../services/complexity.service';
import { StatusService } from '../../../services/status.service';
import { Complexity } from '../../../models/complexity.model';
import { Status } from '../../../models/status.model';
import { NotificationService } from '../../../services/notification.service';
import { ConfirmDialogComponent } from '../../confirm-dialog/confirm-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { DateValidators } from '../../../customvalidators/date-validators';
import { MatDatepicker } from '@angular/material/datepicker';

@Component({
  selector: 'app-feature-form',
  templateUrl: './feature-form.component.html',
  styleUrls: ['./feature-form.component.scss']
})
export class FeatureFormComponent implements OnInit {
  featureForm!: FormGroup;
  isEditMode: boolean = false;
  featureId!: number;
  complexities: Complexity[] = [];
  statuses: Status[] = [];
  yesterday = new Date();
  @ViewChild('picker1') picker1!: MatDatepicker<Date>;
  @ViewChild('picker2') picker2!: MatDatepicker<Date>;
  constructor(
    private fb: FormBuilder,
    private featureService: FeatureService,
    private complexityService: ComplexityService,
    private statusService: StatusService,
    private route: ActivatedRoute,
    private router: Router,
    private notificationService: NotificationService,
    private dialog: MatDialog
  ) {
    this.yesterday.setDate(this.yesterday.getDate() - 0);
  }

  ngOnInit(): void {
    this.featureForm = this.fb.group({
      featuresId: [''],
      title: ['', [Validators.required, Validators.maxLength(1000)]],
      description: ['', [Validators.maxLength(5000)]],
      complexityId: [null],
      statusId: [null],
      targetCompletionDate: [null, [DateValidators.futureDate, this.requiredForActiveStatus.bind(this)]],
      actualCompletionDate: [null, [DateValidators.futureDate, this.requiredForClosedStatus.bind(this)]]

    });
    this.loadDropdownOptions();
    this.onStatusChanges();
    this.route.params.subscribe(params => {
      if (params['id']) {
        this.isEditMode = true;
        this.featureId = params['id'];
        this.loadFeature(this.featureId);
      }
    });
  }
  onStatusChanges(): void {
    this.featureForm.get('statusId')?.valueChanges.subscribe(status => {
      const targetControl = this.featureForm.get('targetCompletionDate');
      const actualControl = this.featureForm.get('actualCompletionDate');

      if (this.statuses.find(x => x.statusId == status)?.statusName === 'Active') {
        targetControl?.setValidators([Validators.required, DateValidators.futureDate, this.requiredForActiveStatus.bind(this)]);
      } else {
        targetControl?.clearValidators();
      }

      if (this.statuses.find(x => x.statusId == status)?.statusName === 'Closed') {
        actualControl?.setValidators([Validators.required, DateValidators.futureDate, this.requiredForClosedStatus.bind(this)]);
      } else {
        actualControl?.clearValidators();
      }

      targetControl?.updateValueAndValidity();
      actualControl?.updateValueAndValidity();
    });
  }

  requiredForActiveStatus(control: AbstractControl): { [key: string]: boolean } | null {
    if (this.featureForm && this.statuses.find(x => x.statusId == this.featureForm.get('statusId')?.value)?.statusName === 'Active' && !control.value) {
      return { 'requiredForActiveStatus': true };
    }
    return null;
  }

  requiredForClosedStatus(control: AbstractControl): { [key: string]: boolean } | null {
    if (this.featureForm && this.statuses.find(x => x.statusId == this.featureForm.get('statusId')?.value)?.statusName === 'Closed' && !control.value) {
      return { 'requiredForClosedStatus': true };
    }
    return null;
  }

  isFormInvalid(): boolean {
    return this.featureForm.invalid;
  }

  loadFeature(id: number): void {
    // Fetch the feature data from the API based on the ID
    this.featureService.getFeatureById(id).subscribe((feature: Feature) => {
      this.featureForm.patchValue({
        featuresId: feature.featuresId,
        title: feature.title,
        description: feature.description,
        complexityId: feature.complexityId,
        statusId: feature.statusId,
        targetCompletionDate: feature.targetCompletionDate,
        actualCompletionDate: feature.actualCompletionDate
      });
    });
  }

  loadDropdownOptions(): void {
    this.complexityService.getAllComplexities().subscribe(data => {
      this.complexities = data;
    });

    this.statusService.getAllStatuses().subscribe(data => {
      this.statuses = data;
    });
  }

  onSubmit(): void {
    if (this.featureForm.valid) {
      const featureData = this.featureForm.value;
      if (this.isEditMode) {
        // Update the existing feature
        this.featureService.updateFeature(featureData).subscribe(() => {
          this.notificationService.showSuccess('Feature updated successfully');
          this.router.navigate(['/features/list']);
        },
          (error) => {
            this.notificationService.showError("Error Feature Updating: " + JSON.stringify(error));
          });
      } else {
        // Create a new feature
        delete featureData.featuresId;
        this.featureService.addFeature(featureData).subscribe(() => {
          this.notificationService.showSuccess('Feature created successfully');
          this.router.navigate(['/features/list']);

        },
          (error) => {
            this.notificationService.showError("Error Feature Adding: " + JSON.stringify(error));
          });
      }
    }
  }

  openConfirmDialog(): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        // User confirmed, trigger API call
        this.deleteFeature();
      }
    });
  }

  deleteFeature(): void {
    this.featureService.deleteFeature(this.featureId).subscribe(response => {
      this.notificationService.showSuccess('Feature deleted successfully');
      this.router.navigate(['/features/list']);
    }, error => {
      this.notificationService.showError('Error deleting feature ' + error);
    });
  }

  openDatepicker(datepicker: MatDatepicker<Date>): void {
    datepicker.open();
  }
}
