// import './rxjs-extensions';
import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule }   from '@angular/forms';
import { HttpModule, JsonpModule  }    from '@angular/http';
// import { AppRoutingModule } from './app-routing.module';

import {TreeModule} from 'angular2-tree-component';
import {ContextMenuModule} from 'angular2-contextmenu';
// import { ContextMenuComponent } from 'angular2-contextmenu/src/contextMenu.component';
// import { ContextMenuService } from 'angular2-contextmenu/src/contextMenu.service';

// Imports for loading & configuring the in-memory web api
import { InMemoryWebApiModule } from 'angular-in-memory-web-api';
import { InMemoryDataService }  from './_backend/in-memory-data.service';

import { AppComponent } from './app.component';
import { SigninComponent } from './_components/signin.component';

import { HeaderComponent } from './_templates/header.component';
import { FooterComponent } from './_templates/footer.component';

import { ProjectService } from './_services/project.service';
import { TaskService } from './_services/task.service';
import { SigninService } from './_services/signin.service';
import { ProjecteditorComponent } from './_components/projecteditor.component';
import { TaskeditorComponent } from './_components/taskeditor.component';
import {TreeTableModule,ToolbarModule, InputTextModule, ButtonModule, AutoCompleteModule} from 'primeng/primeng';

import { DatePickerModule } from 'ng2-datepicker';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    JsonpModule, 
    InMemoryWebApiModule.forRoot(InMemoryDataService, {passThruUnknownUrl: true}),
    TreeModule,
    ContextMenuModule,
    ReactiveFormsModule,
    DatePickerModule,
    TreeTableModule,
    ToolbarModule,
    InputTextModule,
    ButtonModule,
    AutoCompleteModule
    // AppRoutingModule
  ],
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    ProjecteditorComponent,
    TaskeditorComponent,
    SigninComponent
    // ContextMenuComponent
  ],
  providers: [ProjectService, TaskService, SigninService],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
/*
by Akzholtay 2017.
Other projects: http://nakey.kz
*/
