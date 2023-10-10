import { Component, Input } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { ClientService } from '../../services/client.service';
import swal from 'sweetalert2';
import { BankService } from '../../services/bank.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-modal',
  templateUrl: './add-modal.component.html',
  styleUrls: ['./add-modal.component.css']
})
export class AddModalComponent {

  years: string[] = ['2023', '2024', '2025','2026','2027','2028','2029','2030'];
  months: string[] = ['01', '02', '03','04','05','06','07','08', '09', '10', '11', '12'];
  banks: any[] = [];
  isActive: boolean = true;

  form: FormGroup = this.fb.group({
    name: [null, [Validators.required]],
    lastName: [null, [Validators.required]],
    phoneNumber: [null, [Validators.required]],
    occupation: [null, [Validators.required]],
    isActive: [true, [Validators.required]],
    creditCards: this.fb.array([])
  });

  @Input() public id!: number;

  constructor(
    private fb: FormBuilder,
    private modalService: NgbModal,
    private toastr: ToastrService,
    private clientService: ClientService,
    private bankService: BankService
  ) { }

  ngOnInit(): void {
    this.bankService.getAll()
      .subscribe(data => {
        this.banks = data;
      });

      if (this.id) {
        this.clientService.getById(this.id)
        .subscribe(data => {
          this.form.controls['name'].setValue(data.name);
          this.form.controls['lastName'].setValue(data.lastName);
          this.form.controls['phoneNumber'].setValue(data.phoneNumber);
          this.form.controls['occupation'].setValue(data.occupation);
          this.form.controls['isActive'].setValue(data.isActive);
          data.creditCards.forEach((element: any) => {
            this.addCreditCard(element);
          });
          if (!data.isActive) {
            this.isActive = data.isActive;
            this.disableFields();
          }
        });
      }
  }

  private disableFields() {
    this.form.controls['name'].disable();
    this.form.controls['lastName'].disable();
    this.form.controls['phoneNumber'].disable();
    this.form.controls['occupation'].disable();
    if (this.creditCards.length > 0) {
      for (let creditCardForm of this.creditCards.controls) {
        creditCardForm.disable();
      }
    }
  }

  get creditCards() {
    return this.form.controls["creditCards"] as FormArray;
  }

  addCreditCard(card: any = null) {
    const creditCardForm = this.fb.group({
        type: [card?.type || null, Validators.required],
        bank: [card?.bank || null, Validators.required],
        number: [card?.number|| null, Validators.required],
        month: [card?.month || null, Validators.required],
        year: [card?.year || null, Validators.required]
    });
  
    this.creditCards.push(creditCardForm);
  }

  removeCreditCard(index: number) {
    this.creditCards.removeAt(index);
  }

  onCreditCardNumberChange(index: number) {
    const items = this.form.getRawValue().creditCards;
    for (let i = 0; index < items.length; i++) {
      const element = items[i];
      if (i != index && element.number === items[index].number) {
        swal.fire({
          icon: 'error',
          title: 'Error de validacion!',
          text: 'El numero de tarjeta ya se encuentra registrado!',
        });
        this.creditCards.controls[index].get('number')?.setValue(null);
        break;
      }
    }
  }

  onClose() {
    this.modalService.dismissAll();
  }

  onSave() {
    const formValues = this.form.getRawValue();
    const request = {
      ...formValues,
      id: 0,
      phoneNumber: formValues['phoneNumber'].toString(),
      creditCards: formValues.creditCards.map((item: any) => {
        item.bank = +item.bank;
        item.type = +item.type;
        item.number = item.number.toString();
        return item;
      })
    }

    if (!this.id) {
      this.clientService.add(request)
        .subscribe(() => {
          this.modalService.dismissAll();
          this.toastr.success('Operacion realizada satisfactoriamente!', 'Operacion Exitosa!');
        }, error => {
          swal.fire({
            icon: 'error',
            title: 'Error de validacion!',
            text: error.error,
          });
        });
      
    } else {
      request.id = this.id;
      this.clientService.update(request)
        .subscribe(() => {
          this.modalService.dismissAll();
          this.toastr.success('Operacion realizada satisfactoriamente!', 'Operacion Exitosa!');
        }, error => {
          swal.fire({
            icon: 'error',
            title: 'Error de validacion!',
            text: error.error,
          });
        });
    }
  }

}
