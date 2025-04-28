export interface Feature {
    id: number;
    name: string;
    description: string;
    status: FeatureStatus;
    parentFeatureId?: number;
    parentFeatureName?: string;
    creationTime?: Date;
    lastModificationTime?: Date;
}

export type FeatureStatus = 'active' | 'inactive';

export interface PagedResultDto {
    items: Feature[];
    totalCount: number;
}

export interface CreateFeatureDto {
    name: string;
    description: string;
    status: FeatureStatus;
    parentFeatureId?: number;
}

export interface UpdateFeatureDto extends CreateFeatureDto {
    id: number;
}

export interface FeatureFilterDto {
    keyword?: string;
    status?: FeatureStatus;
    parentFeatureId?: number;
    skipCount?: number;
    maxResultCount?: number;
} 