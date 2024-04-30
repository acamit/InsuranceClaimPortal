import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html',
})
export class CounterComponent {
  public currentCount = 0;

  constructor() {}

  public incrementCounter() {
    this.currentCount++;
  }
}
