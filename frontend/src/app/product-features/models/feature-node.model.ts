export interface FeatureNode {
  id: string;
  name: string;
  description?: string;
  children: FeatureNode[];
  isEnabled?: boolean;
  metadata?: {
    [key: string]: any;
  };
} 