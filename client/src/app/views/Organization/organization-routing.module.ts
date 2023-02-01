import { NgModule } from '@angular/core';
import { OrganizationProfileComponent } from '../Organization/organization-profile/organization-profile.component'
import { RouterModule, Routes } from '@angular/router';
import { OrganizationBranchesComponent } from './organization-branches/organization-branches.component';
import { OrganizationStructuresComponent } from './organization-structures/organization-structures.component';

const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Organization'
    },
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'profile'
      },
      {
        path: 'profile',
        component: OrganizationProfileComponent,

        data: {
          title: 'Organization Profile'
        }
      },
      {
        path: 'branches',
        component: OrganizationBranchesComponent,

        data: {
          title: 'Organization Branches'
        }
      }, {
        path: 'structure',
        component: OrganizationStructuresComponent,
        data: {
          title: 'Organization Structures'
        }
      }
    ]
  }
];


@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class OrganizationRoutingModule { }
