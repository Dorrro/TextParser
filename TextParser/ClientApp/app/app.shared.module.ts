import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { ConvertTextComponent } from "./components/convert-text/convert-text.component";

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        ConvertTextComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'convert-text', pathMatch: 'full' },
            { path: 'convert-text', component: ConvertTextComponent },
            { path: '**', redirectTo: 'convert-text' }
        ])
    ]
})
export class AppModuleShared {
}
