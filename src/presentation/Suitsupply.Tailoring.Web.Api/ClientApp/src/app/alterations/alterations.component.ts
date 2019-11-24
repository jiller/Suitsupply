import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Alteration } from "../common/alteration";

@Component({
  selector: 'app-alterations',
  templateUrl: './alterations.component.html'
})
export class AlterationsComponent {
  public alterations: Alteration[];

  constructor(http: HttpClient, @Inject('BASE_API_URL') baseUrl: string) {
    http.get<Alteration[]>(baseUrl + '/alterations').subscribe(result => {
      this.alterations = result;
    }, error => console.error(error));
  }
}
