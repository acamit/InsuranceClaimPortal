import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';
import { Vendor } from '../models/Vendor';

@Component({
  selector: 'app-vendors',
  templateUrl: './vendors.component.html',
  styleUrls: ['./vendors.component.css'],
})
export class VendorsComponent implements OnInit {
  public Vendors: Vendor[] = [];

  constructor(private api: ApiService) {}

  ngOnInit(): void {
    this.api.getAllVendors().subscribe({
      next: (res: any) => {
        this.Vendors = res;
        console.log(this.Vendors);
      },
      error: (err: any) => console.log(err),
    });
  }
}
