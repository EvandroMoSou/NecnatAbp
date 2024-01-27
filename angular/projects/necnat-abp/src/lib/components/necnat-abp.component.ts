import { Component, OnInit } from '@angular/core';
import { NecnatAbpService } from '../services/necnat-abp.service';

@Component({
  selector: 'lib-necnat-abp',
  template: ` <p>necnat-abp works!</p> `,
  styles: [],
})
export class NecnatAbpComponent implements OnInit {
  constructor(private service: NecnatAbpService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
