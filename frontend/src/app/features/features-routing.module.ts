import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FeatureListComponent } from './feature-list/feature-list.component';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';

const routes: Routes = [
    {
        path: '',
        component: FeatureListComponent,
        canActivate: [AppRouteGuard]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class FeaturesRoutingModule { } 