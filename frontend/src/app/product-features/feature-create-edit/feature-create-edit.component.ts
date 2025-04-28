import { Component, Injector, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AppComponentBase } from '@shared/app-component-base';
import { FeatureService } from '../feature.service';
import { Feature } from '../models/feature.model';
import { finalize } from 'rxjs/operators';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
    selector: 'app-feature-create-edit',
    templateUrl: './feature-create-edit.component.html',
    animations: [appModuleAnimation()]
})
export class FeatureCreateEditComponent extends AppComponentBase implements OnInit {
    saving = false;
    loading = false;
    loadingParents = false;
    featureData: Feature = {
        id: 0,
        name: '',
        description: '',
        status: 'active',
        parentFeatureId: null,
        parentFeatureName: ''
    };
    form: FormGroup;
    parentFeatures: Feature[] = [];
    isEditMode = false;

    constructor(
        injector: Injector,
        private _featureService: FeatureService,
        private _fb: FormBuilder,
        private _router: Router,
        private _route: ActivatedRoute
    ) {
        super(injector);
        this.form = this._fb.group({
            name: ['', [Validators.required, Validators.maxLength(256)]],
            description: ['', Validators.maxLength(1000)],
            status: ['active', Validators.required],
            parentFeatureId: [null]
        });
    }

    ngOnInit(): void {
        this.loadParentFeatures();
        const id = this._route.snapshot.params['id'];
        if (id) {
            this.isEditMode = true;
            this.loadFeature(id);
        }
    }

    loadFeature(id: number): void {
        this.loading = true;
        this._featureService.getFeature(id)
            .pipe(
                finalize(() => {
                    this.loading = false;
                })
            )
            .subscribe(
                (result) => {
                    this.featureData = result;
                    this.form.patchValue(result);
                },
                error => {
                    console.error('Error loading feature:', error);
                    this.notify.error(this.l('ErrorLoadingFeature'));
                    this._router.navigate(['/product-features']);
                }
            );
    }

    loadParentFeatures(): void {
        this.loadingParents = true;
        this._featureService.getFeatures()
            .pipe(
                finalize(() => {
                    this.loadingParents = false;
                })
            )
            .subscribe(
                (result) => {
                    this.parentFeatures = (result.items || []).filter(f => f.id !== this.featureData.id);
                },
                error => {
                    console.error('Error loading parent features:', error);
                    this.notify.error(this.l('ErrorLoadingParentFeatures'));
                }
            );
    }

    save(): void {
        if (this.form.invalid) {
            this.notify.error(this.l('PleaseCheckRequiredFields'));
            return;
        }

        this.saving = true;
        const formData = this.form.value;

        if (this.isEditMode) {
            this._featureService
                .updateFeature({ ...this.featureData, ...formData })
                .pipe(
                    finalize(() => {
                        this.saving = false;
                    })
                )
                .subscribe(
                    () => {
                        this.notify.success(this.l('SavedSuccessfully'));
                        this._router.navigate(['/product-features']);
                    },
                    error => {
                        console.error('Error updating feature:', error);
                        this.notify.error(this.l('ErrorUpdatingFeature'));
                    }
                );
        } else {
            this._featureService
                .createFeature(formData)
                .pipe(
                    finalize(() => {
                        this.saving = false;
                    })
                )
                .subscribe(
                    () => {
                        this.notify.success(this.l('SavedSuccessfully'));
                        this._router.navigate(['/product-features']);
                    },
                    error => {
                        console.error('Error creating feature:', error);
                        this.notify.error(this.l('ErrorCreatingFeature'));
                    }
                );
        }
    }

    cancel(): void {
        this._router.navigate(['/product-features']);
    }
} 