import { Component, Injector, OnInit, Output, EventEmitter } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { TestCaseService } from '../test-case.service';
import { TestCase } from '../models/test-case.dto';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-create-or-edit-test-case',
  templateUrl: './create-or-edit-test-case.component.html'
})
export class CreateOrEditTestCaseComponent extends AppComponentBase implements OnInit {
  @Output() onSave = new EventEmitter<any>();
  saving = false;
  testresult: any[] = [];
  testCase: TestCase = {
    id: 0,
    name: '',
    steps: '',
    testData: '',
    expectedOutcome: '',
    status: 'active',
    scenarioId: 0,
    scenarioName: ''
  };
  id: number;
  form: FormGroup;

  constructor(
    injector: Injector,
    private _testCaseService: TestCaseService,
    private _fb: FormBuilder,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
    this.form = this._fb.group({
      name: [''],
      steps: [''],
      testData: [''],
      expectedOutcome: [''],
      status: ['active'],
      scenarioId: [0],
      scenarioName: ['']
    });
  }

  ngOnInit(): void {
    if (this.id) {
      this._testCaseService.getTestCase(this.id).subscribe(
        (result) => {
          console.log('Loaded test case:', result);
          this.testresult = Object.values(result).filter(item => typeof item === 'object' && item !== null);
          result = this.testresult[0];
          if (result) {
            this.testCase = {
              id: result.id,
              name: result.name,
              steps: result.steps,
              testData: result.testData,
              expectedOutcome: result.expectedOutcome,
              status: result.status,
              scenarioId: result.scenarioId,
              scenarioName: result.scenarioName
            };
            this.testCase = result;
            this.form.patchValue(result);
          }
        },
        error => {
          console.error('Error loading test case:', error);
          this.notify.error(this.l('ErrorLoadingTestCase'));
        }
      );
    }
  }

  save(): void {
    this.saving = true;
    const formData = this.form.value;

    if (this.id) {
      this._testCaseService
        .updateTestCase({ ...this.testCase, ...formData })
        .pipe(
          finalize(() => {
            this.saving = false;
          })
        )
        .subscribe(() => {
          this.notify.info(this.l('SavedSuccessfully'));
          this.bsModalRef.hide();
          this.onSave.emit();
        });
    } else {
      this._testCaseService
        .createTestCase(formData)
        .pipe(
          finalize(() => {
            this.saving = false;
          })
        )
        .subscribe(() => {
          this.notify.info(this.l('SavedSuccessfully'));
          this.bsModalRef.hide();
          this.onSave.emit();
        });
    }
  }
} 