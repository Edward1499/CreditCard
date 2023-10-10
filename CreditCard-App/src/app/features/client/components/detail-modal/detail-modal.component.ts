import { Component, Input } from '@angular/core';
import { BankService } from '../../services/bank.service';
import { ClientService } from '../../services/client.service';

@Component({
  selector: 'app-detail-modal',
  templateUrl: './detail-modal.component.html',
  styleUrls: ['./detail-modal.component.css']
})
export class DetailModalComponent {
  
  @Input() public id!: number;
  banks: any[] = [];
  client: any;
  currentBank: string = '';

  constructor(private bankService: BankService,
    private clientService: ClientService) {}

  ngOnInit(): void {
    this.bankService.getAll()
      .subscribe(data => {
        this.banks = data;
      });

      if (this.id) {
        this.clientService.getById(this.id)
        .subscribe(data => {
          this.client = data;
        });
      }
  }

  getBankName(key: number) {
    return this.banks.find(x => x.key == key)?.value;
  }

}
