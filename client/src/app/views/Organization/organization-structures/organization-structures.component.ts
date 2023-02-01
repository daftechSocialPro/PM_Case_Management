import { Component, OnInit } from '@angular/core';
import { OrganizationService } from '../organization.service';

@Component({
  selector: 'app-organization-structures',
  templateUrl: './organization-structures.component.html',
  styleUrls: ['./organization-structures.component.scss']
})
export class OrganizationStructuresComponent implements OnInit {
  dtOptions: DataTables.Settings = {};

  constructor(private orgService: OrganizationService) {

  }

  ngOnInit(): void {

    this.dtOptions = {
      ajax: ({ }, callback) => {
        this.orgService.getOrgStructureList().subscribe(resp => {
          console.log(resp)
          callback({
            data: resp
          });
        });
      },
      columns: [

     {
          title: 'Organization Branch',
          data: 'OrganizationBranch.Name'
        },
        {
          title: 'Parent Structure',
          data: 'ParentStructure&&ParentStructure.StructureName'
        },
        {
          title: 'Structure Name',
          data: 'StructureName'
        },
        {
          title: 'Order',
          data: 'Order'
        },
        {
          title: 'Weight',
          data: 'Weight'
        }]
    }
  }

  public createVisible = false;

  toggleCreateStructure() {
    this.createVisible = !this.createVisible;
  }


  handleCreateStructureChange(event: any) {
    this.createVisible = event;
  }

}
