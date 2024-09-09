import { Component, OnInit } from '@angular/core';
import { FeatureService } from '../../../services/feature.service';
import { FeatureDto } from '../../../models/featuredto.model';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ConfirmDialogComponent } from '../../confirm-dialog/confirm-dialog.component';
import { NotificationService } from '../../../services/notification.service';

@Component({
  selector: 'app-feature-list',
  templateUrl: './feature-list.component.html',
  styleUrls: ['./feature-list.component.scss']
})
export class FeatureListComponent implements OnInit {
  featuresDto: FeatureDto[] = [];
  featuresDtoD!: any;
  displayedColumns: string[] = ['title', 'description', 'complexityName', 'statusName', 'targetCompletionDate', 'actualCompletionDate', 'actions'];

  constructor(private featureService: FeatureService
    , private router: Router
    , private dialog: MatDialog
    , private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.loadFeatures();
  }

  loadFeatures(): void {
    this.featureService.getAllFeatures().subscribe(
      (data) => {
        this.featuresDto = data;
      },
      (error) => {
        this.notificationService.showError('Error loading features: ' + error);
      }
    );
  }

  createFeature() {
    this.router.navigate(['/features/create']);
  }

  editFeature(feature: FeatureDto): void {
    this.router.navigate(['/features/edit/' + feature.featuresId]);
  }

  openConfirmDialog(feature: any): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        // User confirmed, trigger API call
        this.deleteFeature(feature);
      }
    });
  }

  deleteFeature(feature: any): void {
    this.featureService.deleteFeature(feature.featuresId).subscribe(response => {
      this.notificationService.showSuccess('Feature deleted successfully');
    }, error => {
      this.notificationService.showError('Error deleting feature ' + error);
    });
  }
}
