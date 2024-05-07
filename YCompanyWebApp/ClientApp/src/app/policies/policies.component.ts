import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';
import { Policy } from '../models/Policy';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-policies',
  templateUrl: './policies.component.html',
  styleUrls: ['./policies.component.css'],
  providers: [DatePipe],
})
export class PoliciesComponent implements OnInit {
  public Policies: Policy[] = [];

  constructor(private api: ApiService) {}
  ngOnInit(): void {
    this.api.getAllPolicies().subscribe({
      next: (res: any) => {
        this.Policies = res;
      },
      error: (err: any) => console.log(err),
    });
  }
}
