export interface TestCase {
  id: number;
  name: string;
  steps: string;
  testData: string;
  expectedOutcome: string;
  status: string;
  scenarioId: number;
  scenarioName: string;
}

export interface PagedResultDto {
  totalCount: number;
  items: TestCase[];
} 