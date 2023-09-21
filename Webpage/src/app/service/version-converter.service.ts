import { Injectable } from '@angular/core';
import { Report } from '../model/report';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';
import { Observable } from 'rxjs';

const VERSION_CONVERTER_REQUEST_URL =
  environment.versionConverterDefaultUrl + 'AngularConverter';
@Injectable({
  providedIn: 'root',
})
export class VersionConverterService {
  public constructor(private http: HttpClient) {}

  public getAllowedVersions(): Observable<number> {
    return this.http.get<number>(VERSION_CONVERTER_REQUEST_URL);
  }

  public getAllStaticChangesFromVersions(
    versionFrom: number,
    versionTo: number
  ): Observable<Report> {
    const url = `VERSION_CONVERTER_REQUEST_URL?versionFrom=${versionFrom}&versionTo=${versionTo}`;
    return this.http.get<Report>(url);
  }

  public getFileChangesFromVersions(
    file: string,
    versionFrom: number,
    versionTo: number
  ): Observable<Report> {
    const getFileChanges = {
      file,
      versionFrom,
      versionTo,
    };
    const url = `VERSION_CONVERTER_REQUEST_URL?versionFrom=${versionFrom}&versionTo=${versionTo}`;
    return this.http.post<Report>(url, getFileChanges);
  }
}
