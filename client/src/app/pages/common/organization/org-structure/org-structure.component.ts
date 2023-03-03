import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { IndividualConfig } from 'ngx-toastr';
import { CommonService, toastPayload } from 'src/app/common/common.service';
import { OrganizationService } from '../organization.service';
import { AddStructureComponent } from './add-structure/add-structure.component';
import { OrganizationalStructure } from './org-structure';
import { UpdateStructureComponent } from './update-structure/update-structure.component';
import { DiagramComponent } from 'gojs-angular';
import * as go from 'gojs';

declare const jsc: any;
interface IStructureTree {
  key: string;
  name: string;
  parent: string;
}
@Component({
  selector: 'app-org-structure',
  templateUrl: './org-structure.component.html',
  styleUrls: ['./org-structure.component.css'],
})
export class OrgStructureComponent implements OnInit {
  @ViewChild(DiagramComponent, { static: false })
  diagramComponent!: DiagramComponent;
  familyData: IStructureTree[] = [];

  public diagramDivClassName = 'myDiagramDiv';
  public diagramModelData = { prop: 'value', color: 'red' };

  public dia: any;

  @ViewChild('myDiag', { static: false }) myDiag!: DiagramComponent;

  structures: OrganizationalStructure[] = [];
  toast!: toastPayload;
  structure!: OrganizationalStructure;

  constructor(
    private elementRef: ElementRef,
    private orgService: OrganizationService,
    private commonService: CommonService,
    private modalService: NgbModal
  ) {
    this.structureList();
  }

  ngOnInit(): void {
    var s = document.createElement('script');
    s.type = 'text/javascript';
    s.src = '../assets/js/main.js';
    this.elementRef.nativeElement.appendChild(s);
    this.structureList();
  }

  initDiagram(): go.Diagram {
    const $ = go.GraphObject.make;
    const dia = $(go.Diagram, {
      'toolManager.hoverDelay': 100, // 100 milliseconds instead of the default 850
      allowCopy: false,
      // create a TreeLayout for the family tree
      layout: $(go.TreeLayout, {
        angle: 90,
        nodeSpacing: 10,
        layerSpacing: 40,
        layerStyle: go.TreeLayout.LayerUniform,
      }),
    });

    dia.nodeTemplate = $(
      go.Node,
      'Auto',
      {
        deletable: false,
      },
      new go.Binding('text', 'name'),
      $(go.Shape, 'Rectangle', {
        fill: '#f28282',
        strokeWidth: 0,
        stretch: go.GraphObject.Fill,
        alignment: go.Spot.Center,
      }),
      $(
        go.TextBlock,
        {
          font: '700 12px Droid Serif, sans-serif',
          stroke: 'white',
          textAlign: 'center',
          margin: 10,

          maxSize: new go.Size(80, NaN),
        },
        new go.Binding('text', 'name')
      )
    );

    dia.linkTemplate = $(
      go.Link,
      {
        routing: go.Link.Orthogonal,
        corner: 5,
        selectable: false,
      },
      $(go.Shape, {
        strokeWidth: 3,
        stroke: '#424242',
      })
    );

    dia.model = new go.TreeModel(this.familyData);

    this.dia = dia;

    return dia;
  }

  structureList() {
    this.orgService.getOrgStructureList().subscribe({
      next: (res) => {
        this.structures = res;
        res.forEach((el) => {
          this.familyData.push({
            key: el.Id,
            name: `${el.StructureName}\n${el.Weight}% ${
              el.ParentWeight !== null ? `of ${el.ParentWeight}` : ''
            }`,
            parent: el.ParentStructureId,
          });
        });
        console.log(res);
      },
      error: (err) => {
        this.toast = {
          message: 'Something went wrong',
          title: 'Network error.',
          type: 'error',
          ic: {
            timeOut: 2500,
            closeButton: true,
          } as IndividualConfig,
        };
        this.commonService.showToast(this.toast);
      },
    });
  }

  addStructure() {
    let modalRef = this.modalService.open(AddStructureComponent, {
      size: 'lg',
      backdrop: 'static',
    });
    modalRef.result.then(() => {
      this.structureList();
    });
  }
  updateStructure(value: OrganizationalStructure) {
    let modalRef = this.modalService.open(UpdateStructureComponent, {
      size: 'lg',
      backdrop: 'static',
    });

    modalRef.componentInstance.structure = value;

    modalRef.result.then(() => {
      this.structureList();
    });
  }
}
