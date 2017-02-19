import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import {Project} from '../_models/project';
// import {Task} from '../_models/task';
import {ProjectService} from '../_services/project.service';
import {SigninService} from '../_services/signin.service';
import { Token } from '../_models/token';
import {Response} from '@angular/http';

@Component({ 
  moduleId: module.id,
  selector: 'sign-in',
  templateUrl: 'signin.component.html',
  styleUrls: ['signin.component.css']

})
export class SigninComponent implements OnInit{
	usertoken: Token;
  @Output() setToken: EventEmitter<Token> = new EventEmitter<Token>();
  errorMessage: string;

	constructor(
      private projectService: ProjectService,
      private signinService: SigninService
    ) {}

 	ngOnInit(): void{
 	}
 	signIn(username: string, password: string){
 		// console.log(username);
 		// console.log(password);
    this.signinService.signin(username, password).subscribe(
                res => {this.usertoken = res;
                console.log(JSON.stringify(this.usertoken));
                this.setToken.emit(this.usertoken)},
                error => console.log(JSON.stringify(error))
            );
 		//this.usertoken = this.signinService.signin(username, password);
 	}
}
