import { Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-delete-modal',
  templateUrl: './delete-modal.component.html',
  styleUrls: ['./delete-modal.component.css']
})
export class DeleteModalComponent {

  constructor(
    private modalService: NgbModal,
  ) { }

  onClose() {
    this.modalService.dismissAll();
  }

  onClickRemove() {
    this.modalService.dismissAll('remove');
  }

}
