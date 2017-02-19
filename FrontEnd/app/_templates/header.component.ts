import { Component, Input } from '@angular/core';
import { Token } from '../_models/token';
import {SigninService} from '../_services/signin.service';

@Component({
  moduleId: module.id,
  selector: 'my-header',
  templateUrl: 'header.component.html',
  styleUrls: ['header.component.css']
})
export class HeaderComponent{
	@Input() usertoken: Token;

	logout() {
		this.usertoken = null;
		localStorage.removeItem("currentUser");
	}
}
