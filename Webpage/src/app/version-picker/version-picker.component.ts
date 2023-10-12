import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { VersionConverterService } from '../service/version-converter.service';

@Component({
  selector: 'app-version-picker',
  templateUrl: './version-picker.component.html',
  styleUrls: ['./version-picker.component.scss'],
})
export class VersionPickerComponent implements OnInit {
  @Output() versionFromChange = new EventEmitter<number>();
  @Output() versionToChange = new EventEmitter<number>();

  versionsFrom: number[] = [];
  versionsTo: number[] = [];

  constructor(private versionConverterService: VersionConverterService) {}

  ngOnInit(): void {
    this.versionConverterService.getAllowedVersions().subscribe({
      next: (versions) => this.handleAllowedVersions(versions),
      error: (error) => console.log(error),
    });
  }

  private handleAllowedVersions(versions: number[]) {
    const versionsFrom = [ ...versions ]/*.splice(0, 1)*/;
    const versionsTo = [ ...versions ].splice(versions.length - 1, 1);

    this.versionsFrom = versionsFrom;
    this.versionsTo = versionsTo;
  }

  changeVersionFrom(event: Event) {
    const version = + (event.target as HTMLSelectElement).value;
    this.versionFromChange.emit(version);
  }

  changeVersionTo(event: Event) {
    const version = + (event.target as HTMLSelectElement).value;
    this.versionToChange.emit(version);}
}
