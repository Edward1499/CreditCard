import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { AddModalComponent } from '../components/add-modal/add-modal.component';
import { ClientService } from '../services/client.service';
import { DeleteModalComponent } from 'src/app/shared/components/delete-modal/delete-modal.component';
import { DetailModalComponent } from '../components/detail-modal/detail-modal.component';

interface Client {
  id: number;
  name: string;
  lastName: string;
  phoneNumber: number;
  occupation: number;
}

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent {

  constructor(
    private modalService: NgbModal,
    private toastr: ToastrService,
    private clientService: ClientService
  ) {}

  displayedColumns: string[] = ['name', 'lastName', 'phoneNumber', 'occupation', 'isActive', 'actions'];
  dataSource = new MatTableDataSource<Client>([]);

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngOnInit(): void {
    this.getAll();
  }

  private getAll() {
    this.clientService.getAll()
      .subscribe(data => {
        this.dataSource.data = data;
      });
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  open(id?: number) {
    const modalRef = this.modalService.open(AddModalComponent, { ariaLabelledBy: 'modal-basic-title', size: 'xl' });
    modalRef.dismissed.subscribe(() => {
      this.getAll();
    });
    if (id) {
      modalRef.componentInstance.id = id;
    }
	}

  openDetail(id: number) {
    const modalRef = this.modalService.open(DetailModalComponent, { ariaLabelledBy: 'modal-basic-title', size: 'xl' });
    modalRef.componentInstance.id = id;
	}

  onRemove(id: number) {
    this.modalService.open(DeleteModalComponent, { ariaLabelledBy: 'modal-basic-title' }).result
      .then(_ => {}
      ,(reason) => {
        if (reason === 'remove') {
          this.clientService.delete(id)
            .subscribe(() => {
              this.toastr.success('Operacion realizada satisfactoriamente!', 'Operacion Exitosa!');
              this.getAll();
            });
        }
      });
  }

}
