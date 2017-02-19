/**
 * System configuration for Angular samples
 * Adjust as necessary for your application needs.
 */
 // var map = {
 //   'angular2-tree-component':    'node_modules/angular2-tree-component',
 //   'lodash':                     'node_modules/lodash',
 // };
 //
 // var packages = {
 //   'angular2-tree-component'   : { main: 'dist/angular2-tree-component.js', defaultExtension: 'js' },
 //   'lodash'                    : { main: 'lodash.js', defaultExtension: 'js' },
 // };
(function (global) {
  System.config({
    paths: {
      // paths serve as alias
      'npm:': 'node_modules/'
    },
    // map tells the System loader where to look for things
    map: {
      // our app is within the app folder
      app: 'app',

      // angular bundles
      '@angular/core': 'npm:@angular/core/bundles/core.umd.js',
      '@angular/common': 'npm:@angular/common/bundles/common.umd.js',
      '@angular/compiler': 'npm:@angular/compiler/bundles/compiler.umd.js',
      '@angular/platform-browser': 'npm:@angular/platform-browser/bundles/platform-browser.umd.js',
      '@angular/platform-browser-dynamic': 'npm:@angular/platform-browser-dynamic/bundles/platform-browser-dynamic.umd.js',
      '@angular/http': 'npm:@angular/http/bundles/http.umd.js',
      '@angular/router': 'npm:@angular/router/bundles/router.umd.js',
      '@angular/forms': 'npm:@angular/forms/bundles/forms.umd.js',

      // other libraries
      'rxjs':                      'npm:rxjs',
      'angular-in-memory-web-api': 'npm:angular-in-memory-web-api/bundles/in-memory-web-api.umd.js',
      'angular2-tree-component':    'node_modules/angular2-tree-component',
      'angular2-contextmenu':'node_modules/angular2-contextmenu',
      'lodash':                     'node_modules/lodash',
      'ng2-datepicker': 'node_modules/ng2-datepicker',
      'ng2-slimscroll': 'node_modules/ng2-slimscroll',
      'moment': 'node_modules/moment',
      'primeng': 'npm:primeng'
    },
    // packages tells the System loader how to load when no filename and/or no extension
    packages: {
      app: {
        main: './main.js',
        defaultExtension: 'js'
      },
      rxjs: {
        defaultExtension: 'js'
      },
      primeng: {
          defaultExtension: 'js'
      },
      'angular2-tree-component'   : { main: 'dist/angular2-tree-component.js', defaultExtension: 'js' },
      'angular2-contextmenu'   : { main: 'angular2-contextmenu.js', defaultExtension: 'js' },
      'lodash'                    : { main: 'lodash.js', defaultExtension: 'js' },
      'ng2-datepicker'                    : { main: 'ng2-datepicker.js', defaultExtension: 'js' },
      'ng2-slimscroll'                    : { main: 'ng2-slimscroll.js', defaultExtension: 'js' },
      'moment'                    : { main: 'moment.js', defaultExtension: 'js' }
    }
  });
})(this);
