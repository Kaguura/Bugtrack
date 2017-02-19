import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

// import {Project} from '../_models/project';
import {Task} from '../_models/task';
import {User} from '../_models/user';
import {Status} from '../_models/status';
import {TaskType} from '../_models/tasktype';
import {TaskService} from '../_services/task.service';
import {DateModel, DatePickerOptions } from 'ng2-datepicker';
import { FormGroup, FormBuilder } from '@angular/forms';
import * as moment from 'moment';


@Component({ 
  moduleId: module.id,
  selector: 'task-editor',
  templateUrl: 'taskeditor.component.html',
  styleUrls: ['taskeditor.component.css']

})
export class TaskeditorComponent implements OnInit{
	@Input('taskId') taskId: number;
  @Input('isParentId') isParentId: boolean;
  @Input('viewTask') viewTask: boolean;
  @Input() projectId: number;
  @Output() updateList: EventEmitter<string> = new EventEmitter<string>();
	errorMessage: string;
  task: Task = new Task();
	users: User[];
  status: Status[];
  taskTypes: TaskType[];
  startDate: DateModel;
  endDate: DateModel;
  estimatedEndDate: DateModel;
  createdDate: DateModel;
  startDateOptions: DatePickerOptions;
  endDateOptions: DatePickerOptions;
  estimatedEndDateOptions: DatePickerOptions;
  createdDateOptions: DatePickerOptions;
  dataForm: FormGroup;
  initializedDates: boolean = false;
  
    
	constructor(
      private taskService: TaskService,
      private formBuilder: FormBuilder
    ) {}
	
 	ngOnInit(): void{
    this.getTask();
    this.taskService.getUsers().subscribe(users=>this.users = users);
    this.taskService.getStatus().subscribe(status=>this.status = status);
    this.taskService.getTaskTypes().subscribe(types=>this.taskTypes = types);
    
 	}

  initValues(): void {
    this.dataForm = this.formBuilder.group({
      date: ''
    });
    if (!this.task.StartedOn)
      this.startDateOptions = new DatePickerOptions({format: "DD-MM-YYYY",
                                          firstWeekdaySunday: false});
    else this.startDateOptions = new DatePickerOptions({format: "DD-MM-YYYY",
                                          initialDate: new Date(this.task.StartedOn),
                                          firstWeekdaySunday: false});
    if (!this.task.EndedOn)
      this.endDateOptions = new DatePickerOptions({format: "DD-MM-YYYY",
                                          firstWeekdaySunday: false});
    else this.endDateOptions = new DatePickerOptions({format: "DD-MM-YYYY",
                                          initialDate: new Date(this.task.EndedOn),
                                          firstWeekdaySunday: false});
    if (!this.task.EstimatedEndsOn)
      this.estimatedEndDateOptions = new DatePickerOptions({format: "DD-MM-YYYY",
                                          firstWeekdaySunday: false});
    else this.estimatedEndDateOptions = new DatePickerOptions({format: "DD-MM-YYYY",
                                          initialDate: new Date(this.task.EstimatedEndsOn),
                                          firstWeekdaySunday: false});
    if (!this.task.CreatedOn) 
      this.createdDateOptions = new DatePickerOptions({format: "DD-MM-YYYY",
                                          firstWeekdaySunday: false});
    else this.createdDateOptions = new DatePickerOptions({format: "DD-MM-YYYY",
                                          initialDate: new Date(this.task.CreatedOn),
                                          firstWeekdaySunday: false});
    this.initializedDates = true;
  }

 	getTask(): void{
 		if (this.taskId && !this.isParentId)
    		this.taskService.getTaskDetails(this.taskId).subscribe(task=>{this.task = task; this.initValues();});
    else {
      if (this.taskId) {
        this.task.ParentTaskId = this.taskId;
        //this.taskService.getTaskDetails(this.taskId).subscribe(parent=>this.task.ProjectId = parent.ProjectId);
      }
      this.task.ProjectId = this.projectId;
      this.initValues();
    }

  }

  onSubmit() { 
    //console.log('vdfhks' + JSON.stringify(this.task));
    //console.log('date' + this.date.momentObj.toISOString());
    this.task.StartedOn = this.startDate.momentObj.utc(true).toDate();
    console.log('starteddate' + JSON.stringify(this.task.StartedOn));
    if (this.endDate)
      this.task.EndedOn = this.endDate.momentObj.utc(true).toDate();
    if (this.estimatedEndDate)
      this.task.EstimatedEndsOn = this.estimatedEndDate.momentObj.utc(true).toDate();
    if (this.createdDate)
      this.task.CreatedOn = this.startDate.momentObj.utc(true).toDate();
     
 		if (!this.taskId || this.isParentId) {
       this.taskService.addTask(this.task).subscribe(res=>{this.updateList.emit("Successfully saved.");},
                                                     err=>{this.updateList.emit("Failed to add new task.");});
    }
 		else {
       this.taskService.editTask(this.task).subscribe(res=>{this.updateList.emit("Successfully saved.");},
                                                     err=>{this.updateList.emit("Failed to save changes.");});
     }
 	}

  cancel() {
    this.updateList.emit("Cancel");
  }

}
