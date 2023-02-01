import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrganizationRoutingModule } from './organization-routing.module'
import { OrganizationProfileComponent } from './organization-profile/organization-profile.component';
import {
  ButtonModule,
  CardModule,
  FormModule,
  GridModule,
  ModalModule,
  TableModule,

} from '@coreui/angular';
import { DocsComponentsModule } from '@docs-components/docs-components.module';
import {ReactiveFormsModule } from '@angular/forms';
import { DataTablesModule } from 'angular-datatables';
import { OrganizationBranchesComponent } from './organization-branches/organization-branches.component';
import { BranchCreateComponent } from './organization-branches/branch-create/branch-create.component';
import { BranchUpdateComponent } from './organization-branches/branch-update/branch-update.component';
import { OrganizationStructuresComponent } from './organization-structures/organization-structures.component';
import { StructureCreateComponent } from './organization-structures/structure-create/structure-create.component'
@NgModule({
  declarations: [
    OrganizationProfileComponent,
    OrganizationBranchesComponent,
    BranchCreateComponent,
    BranchUpdateComponent,
    OrganizationStructuresComponent,
    StructureCreateComponent
  ],
  imports: [
    CommonModule,
    OrganizationRoutingModule,
    DocsComponentsModule,
    ButtonModule,
    CardModule,
    FormModule,
    DataTablesModule,
    GridModule,
    ReactiveFormsModule,
    TableModule,
    ModalModule
  ]
})

export class OrganizationModule { }
