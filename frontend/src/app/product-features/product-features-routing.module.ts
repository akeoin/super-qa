import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FeatureListComponent } from './feature-list/feature-list.component';
import { FeatureCreateEditComponent } from './feature-create-edit/feature-create-edit.component';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';

const routes: Routes = [
    {
        path: '',
        component: FeatureListComponent,
        canActivate: [AppRouteGuard]
    },
    {
        path: 'create',
        component: FeatureCreateEditComponent,
        canActivate: [AppRouteGuard]
    },
    {
        path: 'edit/:id',
        component: FeatureCreateEditComponent,
        canActivate: [AppRouteGuard]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ProductFeaturesRoutingModule { } 