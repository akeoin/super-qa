import { Component, OnInit, Injector, ChangeDetectorRef } from '@angular/core';
import { TestCaseService } from '../test-case.service';
import { TestCase, PagedResultDto } from '../models/test-case.dto';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { CreateOrEditTestCaseComponent } from '../create-or-edit-test-case/create-or-edit-test-case.component';

class PagedTestCasesRequestDto extends PagedRequestDto {
  keyword: string;
  status: string;
  scenarioId: number;
}

@Component({
  selector: 'app-test-case-list',
  templateUrl: './test-case-list.component.html',
  styleUrls: ['./test-case-list.component.css'],
  animations: [appModuleAnimation()]
})
export class TestCaseListComponent extends PagedListingComponentBase<TestCase> implements OnInit {
  testCases: TestCase[] = [];
  testCases1: any;
  keyword = '';
  status = '';
  scenarioId: number;
  isTableLoading = false;

  constructor(
    private testCaseService: TestCaseService,
    private _modalService: BsModalService,
    injector: Injector,
    cd: ChangeDetectorRef
  ) {
    super(injector, cd);
  }

  ngOnInit(): void {
    this.pageSize = 10;
    this.refresh();
  }

  createTestCase(): void {
    this.showCreateOrEditTestCaseDialog();
  }

  editTestCase(testCase: TestCase): void {
    this.showCreateOrEditTestCaseDialog(testCase.id);
  }

  delete(testCase: TestCase): void {
    abp.message.confirm(
      this.l('TestCaseDeleteWarningMessage', testCase.name),
      undefined,
      (result: boolean) => {
        if (result) {
          this.testCaseService.deleteTestCase(testCase.id).subscribe(() => {
            abp.notify.success(this.l('SuccessfullyDeleted'));
            this.refresh();
          });
        }
      }
    );
  }

  private showCreateOrEditTestCaseDialog(id?: number): void {
    let createOrEditTestCaseDialog: BsModalRef;
    if (!id) {
      createOrEditTestCaseDialog = this._modalService.show(
        CreateOrEditTestCaseComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      createOrEditTestCaseDialog = this._modalService.show(
        CreateOrEditTestCaseComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }

    createOrEditTestCaseDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }

  list(
    request: PagedTestCasesRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    this.isTableLoading = true;
    request.keyword = this.keyword;
    request.status = this.status;
    request.scenarioId = this.scenarioId;

    this.testCaseService
      .getTestCases(
        request.keyword,
        request.status,
        request.scenarioId,
        request.skipCount,
        request.maxResultCount
      )
      .pipe(
        finalize(() => {
          this.isTableLoading = false;
          finishedCallback();
        })
      )
      .subscribe(
        (result: PagedResultDto) => {
          console.log('API Response:', result);
          if (result) {
            if (Array.isArray(result)) {
              this.testCases = result;
            } else if (result.items && Array.isArray(result.items)) {
              this.testCases = result.items;
            } else if (typeof result === 'object') {
              this.testCases1 = Object.values(result).filter(item => typeof item === 'object' && item !== null);
              this.testCases = this.testCases1[0].items;
            } else {
              this.testCases = [];
            }
          } else {
            this.testCases = [];
          }
          console.log('Test Cases after conversion:', this.testCases);
          this.showPaging(result, pageNumber);
          this.cd.detectChanges();
        },
        error => {
          console.error('Error loading test cases:', error);
          this.notify.error(this.l('ErrorLoadingTestCases'));
          this.testCases = [];
          this.cd.detectChanges();
        }
      );
  }

  getStatusText(status: string): string {
    switch (status) {
      case 'active':
        return this.l('Active');
      case 'inactive':
        return this.l('Inactive');
      default:
        return this.l('Unknown');
    }
  }
} 