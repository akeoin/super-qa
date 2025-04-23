export interface Feature {
    id?: number;
    name: string;
    description?: string;
    projectId: number;
    isActive: boolean;
    creationTime?: string;
    creatorUserId?: number;
    lastModificationTime?: string;
    lastModifierUserId?: number;
} 