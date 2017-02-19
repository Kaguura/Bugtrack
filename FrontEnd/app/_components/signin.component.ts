import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import {Project} from '../_models/project';
// import {Task} from '../_models/task';
import {ProjectService} from '../_services/project.service';
import {SigninService} from '../_services/signin.service';
import { Token } from '../_models/token';
import {Response} from '@angular/http';
import { Message } from 'primeng/primeng';

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
  msgs: Message[] = [];

	constructor(
      private projectService: ProjectService,
      private signinService: SigninService
    ) {}

 	ngOnInit(): void{
 	}
 	signIn(username: string, password: string){
    this.msgs = [];
 		// console.log(username);
 		// console.log(password);
    this.signinService.signin(username, password).subscribe(
                res => {this.usertoken = res;
                this.msgs.push({severity:'success', summary:'Success', detail:'Redirecting...'});
                console.log(JSON.stringify(this.usertoken));
                this.setToken.emit(this.usertoken)},
                error => {
                  if (error == 400) this.msgs.push({severity:'error', summary:'Error', detail:'Invalid login or password'});
                  else this.msgs.push({severity:'error', summary:'Error', detail:'Connection refused'});
                }
            );
 		//this.usertoken = this.signinService.signin(username, password);
 	}
}
