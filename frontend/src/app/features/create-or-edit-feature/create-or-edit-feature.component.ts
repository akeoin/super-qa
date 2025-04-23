import { Component, Injector, OnInit, Output, EventEmitter } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { FeatureService } from '../feature.service';
import { Feature } from '../models/feature.dto';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-create-or-edit-feature',
  templateUrl: './create-or-edit-feature.component.html'
})
export class CreateOrEditFeatureComponent extends AppComponentBase implements OnInit {
  @Output() onSave = new EventEmitter<any>();
  saving = false;
  id?: number;
  form: FormGroup;

  constructor(
    injector: Injector,
    private _featureService: FeatureService,
    private _fb: FormBuilder,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
    this.form = this._fb.group({
      name: ['', [Validators.required, Validators.maxLength(128)]],
      description: ['', Validators.maxLength(500)],
      isActive: [true],
      projectId: [null, Validators.required]
    });
  }

  ngOnInit(): void {
    if (this.id) {
      this._featureService.getFeature(this.id).subscribe(
        (result: Feature) => {
          this.form.patchValue(result);
        },
        error => {
          this.notify.error(this.l('ErrorLoadingFeature'));
          this.bsModalRef.hide();
        }
      );
    }
  }

  save(): void {
    if (this.form.invalid) {
      this.notify.error(this.l('PleaseCheckTheForm'));
      return;
    }

    this.saving = true;
    const formData = this.form.value as Feature;

    if (this.id) {
      this._featureService
        .updateFeature({ ...formData, id: this.id })
        .pipe(
          finalize(() => {
            this.saving = false;
          })
        )
        .subscribe({
          next: () => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.bsModalRef.hide();
            this.onSave.emit();
          },
          error: (error) => {
            this.notify.error(this.l('ErrorSavingFeature'));
          }
        });
    } else {
      this._featureService
        .createFeature(formData)
        .pipe(
          finalize(() => {
            this.saving = false;
          })
        )
        .subscribe({
          next: () => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.bsModalRef.hide();
            this.onSave.emit();
          },
          error: (error) => {
            this.notify.error(this.l('ErrorSavingFeature'));
          }
        });
    }
  }
} 