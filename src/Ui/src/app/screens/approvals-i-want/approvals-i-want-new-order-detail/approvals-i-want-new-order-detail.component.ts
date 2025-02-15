import {Component, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {FormArray, FormBuilder, FormControl, FormGroup, NgForm, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {NgxSmartModalService} from "ngx-smart-modal";
import "../../../extensions/ng-form.extensions";
import {NewOrderService} from "../../../services/new-order.service";
import {NewApprovalOrder} from "../../../models/new-approval-order";
import {Subject, takeUntil} from "rxjs";

@Component({
  selector: 'app-approvals-i-want-new-order-detail',
  templateUrl: './approvals-i-want-new-order-detail.component.html',
  styleUrls: ['./approvals-i-want-new-order-detail.component.scss']
})
export class ApprovalsIWantNewOrderDetailComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject();
  showUpdatePanel: boolean = false;
  showDocumentAddPanel: boolean = false;
  panelTitle: string = 'Yeni Belge Ekle';
  isEditing: boolean = false;
  submitted = false;
  selectedFileName;
  approvalSubmitted = false;
  newDocumentSubmitted = false;
  actionsHasError = false;
  formGroup: FormGroup;
  formGroupApproval: FormGroup;
  formNewDocument: FormGroup;
  approvalFormValidationMessage = '';
  showChoiceAddPanel: boolean = false;
  model: NewApprovalOrder;
  selectedDocumentIndex: number;
  approvalButtonText: string = 'Kaydet';

  constructor(private newOrderService: NewOrderService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute, private modal: NgxSmartModalService) {
    this.initModel();
    this.formNewDocument = this.fb.group({
      type: ['', Validators.required],
      actions: this.fb.array([]),
      file: '',
      fileName: '',
      title: '',
      content: '',
      formId: '',
      identityNo: '',
      nameSurname: '',
      choiceText: ''
    });
    this.formGroup = this.fb.group({
      title: [this.model.title, Validators.required],
      reference: this.fb.group({
        process: [this.model.reference.process, Validators.required],
        state: [this.model.reference.state, Validators.required],
        processNo: [this.model.reference.processNo, Validators.required],
      }),
      config: this.fb.group({
        expireInMinutes: [this.model.config.expireInMinutes],
        retryFrequence: [this.model.config.retryFrequence, Validators.required],
        maxRetryCount: [this.model.config.maxRetryCount],
      })
    });
    this.formGroupApproval = this.fb.group({
      type: ['', Validators.required],
      value: ''
    });
  }

  ngOnInit(): void {
  }

  ngOnDestroy() {
    this.destroy$.next(true);
    this.destroy$.complete();
  }

  initModel() {
    this.model = this.newOrderService.getModel();
    this.model.documents = this.model.documents.filter(i => i.type != 1);
    this.approvalButtonText = this.model.approver && this.model.approver.nameSurname ? 'Güncelle' : 'Onaycı Ekle';
    this.newOrderService.setModel(this.model);
  }

  getDocumentName(type: number, i: any) {
    switch (type) {
      case 1:
        return i.fileName;
      case 2:
        return i.title;
      case 3:
        return 'Sigorta Onay Formu';
      default:
        return '';
    }
  }

  getState(i: string) {
    if (i === '1')
      return 'ONAY';
    return 'RET';
  }

  getType(i: number) {
    switch (i) {
      case 1:
        return 'Belge';
      case 2:
        return 'Metin';
      case 3:
        return 'Form';
      default:
        return '';
    }
  }

  addChoice() {
    if (!this.formNewDocument.get('choiceText')?.value)
      return;
    (<FormArray>this.formNewDocument.get('actions')).push(this.fb.group({
      title: this.formNewDocument.get('choiceText')?.value,
      choice: '1'
    }));
    this.formNewDocument.get('choiceText')?.setValue('');
    this.actionsHasError = false;
  }

  deleteChoice(i: number) {
    (<FormArray>this.formNewDocument.get('actions')).controls.splice(i, 1);
  }

  editDocument(document: any, index: number) {
    this.isEditing = true;
    this.selectedDocumentIndex = index;
    this.formNewDocument.patchValue({
      type: document.type,
      title: document.title,
      content: document.content,
      formId: document.formId,
      identityNo: document.identityNo,
      nameSurname: document.nameSurname,
      file:document.file,
      fileName: document.fileName
    });
    this.selectedFileName = document.fileName;
    document.actions.forEach(k => {
      (<FormArray>this.formNewDocument.get('actions')).push(this.fb.group({
        title: k.title,
        choice: k.choice
      }))
    });

    this.formNewDocument.get('actions').updateValueAndValidity();
    this.panelTitle = 'Belgeyi Düzenle';
    this.formNewDocument.controls.file.setValidators(null);
    this.formNewDocument.controls.file.updateValueAndValidity();
    this.showDocumentAddPanel = true;
  }

  closeAddPanel() {
    this.formNewDocument.reset();
    this.newDocumentSubmitted = false;
    this.isEditing = false;
    this.panelTitle = 'Yeni Belge Ekle';
    this.selectedFileName = '';
    Object.keys(this.formNewDocument.controls).forEach((key, index) => {
      this.formNewDocument.controls[key].setErrors(null);
      if (key != 'actions')
        this.formNewDocument.controls[key].setValue('');
      else {
        (<FormArray>this.formNewDocument.controls[key]) = this.fb.array([]);
      }
    });
    this.showDocumentAddPanel = false;
  }


  deleteDocument() {
    this.model.documents.splice(this.selectedDocumentIndex, 1);
    this.newOrderService.setModel(this.model);
    this.modal.close('documentDelete');
  }

  deleteDocumentModal(i: number) {
    this.selectedDocumentIndex = i;
    this.modal.open('documentDelete');
  }

  get f() {
    return this.formGroup.controls;
  }

  get fr() {
    return (<FormGroup>this.formGroup.controls.reference).controls;
  }

  get fc() {
    return (<FormGroup>this.formGroup.controls.config).controls;
  }

  get fa() {
    return this.formGroupApproval.controls;
  }

  get fnd() {
    return this.formNewDocument.controls;
  }

  getActions() {
    return (<FormArray>this.formNewDocument.controls.actions).controls;
  }

  onSubmit() {
    this.submitted = true;
    if (this.formGroup.valid) {
      this.newOrderService.save(this.model).pipe(takeUntil(this.destroy$)).subscribe(res => {
        console.log(res);
      });
      this.modal.open('success');
    }
  }

  onSubmitApproval() {
    this.approvalSubmitted = true;
    if (this.formGroupApproval.valid) {
      this.model.approver = {
        type: this.fa.type.value,
        value: this.fa.value.value,
        nameSurname: 'Uğur Karataş'
      }
      this.approvalButtonText = 'Güncelle';
      this.newOrderService.setModel(this.model);
      this.closeApproverPanel();
    }
  }

  closeApproverPanel() {
    this.formGroupApproval.reset();
    this.approvalSubmitted = false;
    Object.keys(this.formGroupApproval.controls).forEach((key, index) => {
      this.formGroupApproval.controls[key].setErrors(null);
      this.formGroupApproval.controls[key].setValue('');
    });
    this.showUpdatePanel = false;
  }

  onSubmitAddDocument() {
    this.newDocumentSubmitted = true;
    if (!(<FormArray>this.formNewDocument.get('actions')).length) {
      this.actionsHasError = true;
      return;
    }
    if (this.formNewDocument.invalid)
      return;

    if (this.isEditing) {
      this.model.documents[this.selectedDocumentIndex] = Object.assign({}, {
        ...this.formNewDocument.getRawValue()
      })
    } else {
      this.model.documents.push({
        ...this.formNewDocument.getRawValue()
      });
    }
    this.newOrderService.setModel(this.model);
    this.closeAddPanel();
  }

  choose(e: any) {
    if (!this.isEditing) {
      this.formNewDocument.controls.file.setValidators(e === 1 ? [Validators.required] : null);
      this.formNewDocument.controls.file.updateValueAndValidity();
    }
    this.formNewDocument.controls.title.setValidators(e === 2 ? [Validators.required] : null);
    this.formNewDocument.controls.title.updateValueAndValidity();
    this.formNewDocument.controls.content.setValidators(e === 2 ? [Validators.required] : null);
    this.formNewDocument.controls.content.updateValueAndValidity();
    this.formNewDocument.controls.formId.setValidators(e === 3 ? [Validators.required] : null);
    this.formNewDocument.controls.formId.updateValueAndValidity();
    this.formNewDocument.controls.identityNo.setValidators(e === 3 ? [Validators.required] : null);
    this.formNewDocument.controls.identityNo.updateValueAndValidity();
    this.formNewDocument.controls.nameSurname.setValidators(e === 3 ? [Validators.required] : null);
    this.formNewDocument.controls.nameSurname.updateValueAndValidity();
  }

  onClickUploadDocument(event: any) {
    const reader = new FileReader();
    if (event.target.files && event.target.files.length) {
      const [file] = event.target.files;
      this.selectedFileName = file.name;
      reader.readAsDataURL(file);
      reader.onload = () => {
        this.formNewDocument.patchValue({
          file: reader.result,
          fileName: this.selectedFileName
        });
      };
    }
  }

  rdoChanged() {
    if (this.formGroupApproval.controls.type.value === 1) {
      this.approvalFormValidationMessage = 'TCKN girilmelidir.'
      this.formGroupApproval.controls.value.setValidators([Validators.required, Validators.minLength(11)]);
      this.formGroupApproval.controls.value.updateValueAndValidity();
    } else {
      this.approvalFormValidationMessage = 'Müşteri No girilmelidir.';
      this.formGroupApproval.controls.value.setValidators(Validators.required);
      this.formGroupApproval.controls.value.updateValueAndValidity();
    }
  }
}
