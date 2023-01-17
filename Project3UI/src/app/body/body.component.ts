import { User } from './../objects/User';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Subject } from 'rxjs/internal/Subject';

@Component({
  selector: 'app-body',
  templateUrl: './body.component.html',
  styleUrls: ['./body.component.css']
})
export class BodyComponent implements OnInit {

  @Input() user: User | null = null;

  @Input() changeUserRoleSubject: Subject<number> = new Subject<number>();

  @Output() onLoginClicked: EventEmitter<void> = new EventEmitter<void>();

  constructor() { }

  ngOnInit(): void {
  }

  onLogin() {
    this.onLoginClicked.emit();
  }

}
