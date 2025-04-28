import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '@shared/shared.module';
import { ServiceProxyModule } from '@shared/service-proxies/service-proxy.module';
import { ModalModule } from 'ngx-bootstrap/modal';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ProductFeaturesRoutingModule } from './product-features-routing.module';
import { FeatureListComponent } from './feature-list/feature-list.component';
import { FeatureCreateEditComponent } from './feature-create-edit/feature-create-edit.component';
import { FeatureTreeComponent } from './feature-tree/feature-tree.component';

@NgModule({
    declarations: [
        FeatureListComponent,
        FeatureCreateEditComponent,
        FeatureTreeComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        SharedModule,
        ServiceProxyModule,
        ModalModule.forRoot(),
        BsDropdownModule.forRoot(),
        ProductFeaturesRoutingModule
    ]
})
export class ProductFeaturesModule { } 