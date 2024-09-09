export interface FeatureDto {
  featuresId: number;
  title: string;
  description?: string;
  complexityId: number;
  complexityName: string; // Added for the complexity name
  statusId: number;
  statusName: string; // Added for the status name
  targetCompletionDate?: Date;
  actualCompletionDate?: Date;
}
