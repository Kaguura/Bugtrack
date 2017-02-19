import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import {Project} from '../_models/project';
// import {Task} from '../_models/task';
import {ProjectService} from '../_services/project.service';

@Component({ 
  moduleId: module.id,
  selector: 'project-editor',
  templateUrl: 'projecteditor.component.html',
  styleUrls: ['projecteditor.component.css']

})
export class ProjecteditorComponent implements OnInit{
	@Input('project') project: Project;
	@Output() updateList: EventEmitter<Project> = new EventEmitter<Project>();

	constructor(
      private projectService: ProjectService
    ) {}

 	ngOnInit(): void{
 	}
 	onSubmit() { 
 		console.log("pr " + this.project.Name);
 		if (!this.project.Id) {
 			//this.projectService.addProject(this.project);
 			this.updateList.emit(this.project);
 		}
 		else this.projectService.editProject(this.project);
 		//alert!
 	}
}
