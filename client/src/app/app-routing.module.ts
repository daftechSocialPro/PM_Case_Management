import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { AlertsComponent } from './components/alerts/alerts.component';
import { AccordionComponent } from './components/accordion/accordion.component';
import { BadgesComponent } from './components/badges/badges.component';
import { BreadcrumbsComponent } from './components/breadcrumbs/breadcrumbs.component';
import { ButtonsComponent } from './components/buttons/buttons.component';
import { CardsComponent } from './components/cards/cards.component';
import { CarouselComponent } from './components/carousel/carousel.component';
import { ChartsApexchartsComponent } from './components/charts-apexcharts/charts-apexcharts.component';
import { ChartsChartjsComponent } from './components/charts-chartjs/charts-chartjs.component';
import { FormsEditorsComponent } from './components/forms-editors/forms-editors.component';
import { FormsElementsComponent } from './components/forms-elements/forms-elements.component';
import { FormsLayoutsComponent } from './components/forms-layouts/forms-layouts.component';
import { IconsBootstrapComponent } from './components/icons-bootstrap/icons-bootstrap.component';
import { IconsBoxiconsComponent } from './components/icons-boxicons/icons-boxicons.component';
import { IconsRemixComponent } from './components/icons-remix/icons-remix.component';
import { ListGroupComponent } from './components/list-group/list-group.component';
import { ModalComponent } from './components/modal/modal.component';
import { PaginationComponent } from './components/pagination/pagination.component';
import { ProgressComponent } from './components/progress/progress.component';
import { SpinnersComponent } from './components/spinners/spinners.component';
import { TablesDataComponent } from './components/tables-data/tables-data.component';
import { TablesGeneralComponent } from './components/tables-general/tables-general.component';
import { TabsComponent } from './components/tabs/tabs.component';
import { TooltipsComponent } from './components/tooltips/tooltips.component';
import { PagesBlankComponent } from './pages/pages-blank/pages-blank.component';
import { PagesContactComponent } from './pages/pages-contact/pages-contact.component';
import { PagesError404Component } from './pages/pages-error404/pages-error404.component';
import { PagesFaqComponent } from './pages/pages-faq/pages-faq.component';
import { PagesLoginComponent } from './pages/pages-login/pages-login.component';
import { PagesRegisterComponent } from './pages/pages-register/pages-register.component';
import { UsersProfileComponent } from './pages/users-profile/users-profile.component';
import { AuthGuard } from './auth/auth.guard';
import { OrgProfileComponent } from './pages/common/organization/org-profile/org-profile.component';
import { OrgBranchComponent } from './pages/common/organization/org-branch/org-branch.component';
import { OrgStructureComponent } from './pages/common/organization/org-structure/org-structure.component';
import { BudgetYearComponent } from './pages/common/budget-year/budget-year.component';
import { EmployeeComponent } from './pages/common/organization/employee/employee.component';
import { UnitMeasurementComponent } from './pages/common/unit-measurement/unit-measurement.component';
import { ArchiveManagementComponent } from './pages/common/archive-management/archive-management.component';
import { UserManagementComponent } from './pages/common/user-management/user-management.component';
import { ProgramsComponent } from './pages/PM/programs/programs.component';
import { PlansComponent } from './pages/PM/plans/plans.component';
import { TasksComponent } from './pages/PM/tasks/tasks.component';
import { ActivityParentsComponent } from './pages/pm/activity-parents/activity-parents.component';
import { EncodeCaseComponent } from './pages/case/encode-case/encode-case.component';
import { ComittesComponent } from './pages/pm/comittes/comittes.component';
import { AssignedActivitiesComponent } from './pages/pm/assigned-activities/assigned-activities.component';
import { CaseTypeComponent } from './pages/case/case-type/case-type.component';
import { FileSettingComponent } from './pages/case/file-setting/file-setting.component';
import { ActivityforapprovalComponent } from './pages/pm/activityforapproval/activityforapproval.component';
import { MyCaseListComponent } from './pages/case/my-case-list/my-case-list.component';
import { CaseDetailComponent } from './pages/Case/case-detail/case-detail.component';
import { CaseHistoryComponent } from './pages/Case/case-history/case-history.component';



const routes: Routes = [


  { path: '', canActivate: [AuthGuard], component: DashboardComponent },
  { path: 'dashboard', canActivate: [AuthGuard], component: DashboardComponent },
  { path: 'orgprofile', canActivate: [AuthGuard], component: OrgProfileComponent },
  { path: 'orgbranch', canActivate: [AuthGuard], component: OrgBranchComponent },
  { path: 'orgstructure', canActivate: [AuthGuard], component: OrgStructureComponent },
  { path: 'budgetyear', canActivate: [AuthGuard], component: BudgetYearComponent },
  { path: 'employee', canActivate: [AuthGuard], component: EmployeeComponent },
  { path: 'unitmeasurment', canActivate: [AuthGuard], component: UnitMeasurementComponent },
  { path: 'archive', canActivate: [AuthGuard], component: ArchiveManagementComponent },
  { path: 'usermanagement', canActivate: [AuthGuard], component: UserManagementComponent },
  { path: 'program', canActivate:[AuthGuard],component:ProgramsComponent},
  { path: 'plan', canActivate:[AuthGuard],component:PlansComponent},
  { path: 'task',canActivate:[AuthGuard],component:TasksComponent},  
  { path: 'activityparent', canActivate:[AuthGuard],component:ActivityParentsComponent},
  { path: 'encodecase' ,canActivate:[AuthGuard],component : EncodeCaseComponent},
  { path: 'comittee' ,canActivate : [AuthGuard],component: ComittesComponent},
  { path: 'assignedactivities' , canActivate:[AuthGuard], component: AssignedActivitiesComponent },
  { path: 'casetype' ,canActivate:[AuthGuard],component : CaseTypeComponent},
  { path: 'filesetting' ,canActivate:[AuthGuard],component : FileSettingComponent},
  { path: 'actForApproval' ,canActivate:[AuthGuard],component : ActivityforapprovalComponent},
  { path: 'mycaselist', canActivate:[AuthGuard], component:MyCaseListComponent},
  { path: 'casedetail',canActivate:[AuthGuard],component:CaseDetailComponent},
  { path: 'caseHistory',canActivate:[AuthGuard],component:CaseHistoryComponent},
  
  { path: 'alerts', component: AlertsComponent },
  { path: 'accordion', component: AccordionComponent },
  { path: 'badges', component: BadgesComponent },
  { path: 'breadcrumbs', component: BreadcrumbsComponent },
  { path: 'buttons', component: ButtonsComponent },
  { path: 'cards', component: CardsComponent },
  { path: 'carousel', component: CarouselComponent },
  { path: 'charts-apexcharts', component: ChartsApexchartsComponent },
  { path: 'charts-chartjs', component: ChartsChartjsComponent },
  { path: 'form-editors', component: FormsEditorsComponent },
  { path: 'form-elements', component: FormsElementsComponent },
  { path: 'form-layouts', component: FormsLayoutsComponent },
  { path: 'icons-bootstrap', component: IconsBootstrapComponent },
  { path: 'icons-boxicons', component: IconsBoxiconsComponent },
  { path: 'icons-remix', component: IconsRemixComponent },
  { path: 'list-group', component: ListGroupComponent },
  { path: 'modal', component: ModalComponent },
  { path: 'pagination', component: PaginationComponent },
  { path: 'progress', component: ProgressComponent },
  { path: 'spinners', component: SpinnersComponent },
  { path: 'tables-data', component: TablesDataComponent },
  { path: 'tables-general', component: TablesGeneralComponent },
  { path: 'tabs', component: TabsComponent },
  { path: 'tooltips', component: TooltipsComponent },
  { path: 'pages-blank', component: PagesBlankComponent },
  { path: 'pages-contact', component: PagesContactComponent },
  { path: 'pages-error404', component: PagesError404Component },
  { path: 'pages-faemploye', component: PagesFaqComponent },
  { path: 'pages-login', component: PagesLoginComponent },
  { path: 'pages-register', component: PagesRegisterComponent },
  { path: 'user-profile', component: UsersProfileComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
