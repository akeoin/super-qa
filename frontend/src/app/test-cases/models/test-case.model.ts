export interface TestCase {
  id?: number;
  title: string;
  description: string;
  steps: string;
  expectedResult: string;
  actualResult?: string;
  status: TestCaseStatus;
  priority: TestCasePriority;
  createdBy?: string;
  creationTime?: Date;
  lastModificationTime?: Date;
  lastModifierUserId?: number;
}

export enum TestCaseStatus {
  New = 0,
  InProgress = 1,
  Passed = 2,
  Failed = 3,
  Blocked = 4
}

export enum TestCasePriority {
  Low = 0,
  Medium = 1,
  High = 2,
  Critical = 3
} 