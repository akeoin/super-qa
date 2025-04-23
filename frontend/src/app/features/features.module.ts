import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { FeaturesRoutingModule } from './features-routing.module';
import { FeatureListComponent } from './feature-list/feature-list.component';
import { CreateOrEditFeatureComponent } from './create-or-edit-feature/create-or-edit-feature.component';

@NgModule({
    declarations: [
        FeatureListComponent,
        CreateOrEditFeatureComponent
    ],
    imports: [
        SharedModule,
        FeaturesRoutingModule
    ]
})
export class FeaturesModule { } 