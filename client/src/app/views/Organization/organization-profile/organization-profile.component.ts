import { HttpEventType } from '@angular/common/http';
import { Component, OnInit, Output } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { CommonService,toastPayload } from 'src/app/shared/common.service';

import { OrganizationService } from '../organization.service'
import { IndividualConfig } from 'ngx-toastr';
@Component({
  selector: 'app-organization-profile',
  templateUrl: './organization-profile.component.html',
  styleUrls: ['./organization-profile.component.scss']
})
export class OrganizationProfileComponent implements OnInit {

  public message: string = '';
  public progress: number = 0;
  //@Output() public onUploadFinished = new EventEmitter();
  toast!: toastPayload;
  imageURL: string = "";
  uploadForm!: UntypedFormGroup;

  constructor(private orgService: OrganizationService, private common:CommonService,private toastr: CommonService,public fb: UntypedFormBuilder) {

    this.uploadForm = this.fb.group({
      id :[null],
      logo:[''],
      avatar: [null],
      organizationNameEnglish: [''],
      organizationNameInLocalLanguage: [''],
      address: [''],
      phoneNumber: [''],
      remark :[''],
      smscode:[''],
      password:[''],
      username:['']


    })

  }


  ngOnInit(): void {

    this.orgService.getOrganizationalProfile().subscribe({
      next:(res)=>{
        if (res!=null){
          this.imageURL =this.common.createImgPath(res.Logo) 
          this.uploadForm = this.fb.group({
            id : res.Id,
            logo:res.Logo,
            avatar: [null],
            organizationNameEnglish: res.OrganizationNameEnglish,
            organizationNameInLocalLanguage: res.OrganizationNameInLocalLanguage,
            address: res.Address,
            phoneNumber: res.PhoneNumber,
            remark :res.Remark,
            smscode:res.SmsCode,
            password:res.Password,
            username:res.UserName
          })
        }
      }
    })
      
      

  }

  // Image Preview
  showPreview(event: any) {
    const file = (event.target).files[0];
    console.log(file)
    this.uploadForm.patchValue({
      avatar: file
    });
    this.uploadForm.get('avatar')?.updateValueAndValidity()
    // File Preview
    const reader = new FileReader();
    reader.onload = () => {
      this.imageURL = reader.result as string;
    }
    reader.readAsDataURL(file)
  }
  // Submit Form
  submit() {
    console.log(this.uploadForm.value)

    var value = this.uploadForm.value;
    var file = value.avatar



    // if (files.length === 0)
    //   return;
    // let fileToUpload = <File>files[0];
    const formData = new FormData();
    file ?formData.append('file', file, file.name):"";
    formData.set('Id',value.id);
    formData.set('logo',value.logo);
    formData.set('organizationNameEnglish', value.organizationNameEnglish);
    formData.set('organizationNameInLocalLanguage', value.organizationNameInLocalLanguage);
    formData.set('address', value.address);
    formData.set('phoneNumber', value.phoneNumber);
    formData.set('remark',value.remark);
    formData.set('SmsCode',value.smscode);
    formData.set('Password',value.password);
    formData.set('UserName',value.username)

   value.id ?
   
   this.orgService.OrganizationUpdate(formData).subscribe((event: any) => {
    if (event.type === HttpEventType.UploadProgress) {
      this.progress = Math.round(100 * event.loaded / event.total);
    }
    else if (event.type === HttpEventType.Response) {
      this.toast = {
        message: 'Organizational Profile Successfully Updated',
        title: 'Successfully Updated.',
        type: 'success',
        ic: {
          timeOut: 2500,
          closeButton: true,
        } as IndividualConfig,
      };
      this.toastr.showToast(this.toast);
    }
  }):

   this.orgService.OrganizationCreate(formData)
      .subscribe((event: any) => {
        if (event.type === HttpEventType.UploadProgress) {
          this.progress = Math.round(100 * event.loaded / event.total);
        }
        else if (event.type === HttpEventType.Response) {
          this.message = 'Upload success.';
          // this.onUploadFinished.emit(event.body);
        }
      })

  }
}
