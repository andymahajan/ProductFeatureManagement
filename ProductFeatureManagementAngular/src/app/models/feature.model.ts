export interface Feature {
  featuresId: number;
  title: string;
  description?: string;
  complexityId?: number;
  statusId?: number;
  targetCompletionDate?: Date;
  actualCompletionDate?: Date;
}
