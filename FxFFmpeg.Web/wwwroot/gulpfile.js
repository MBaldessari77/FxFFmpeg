var gulp = require('gulp');

gulp.task('css', function () {
  return gulp
    .src([
      './node_modules/angular-material/angular-material.css',
      './node_modules/font-awesome/css/font-awesome.css'
    ])
    .pipe(gulp.dest('./css'));
});

gulp.task('fonts', function () {
  return gulp
    .src(['./node_modules/font-awesome/fonts/*.{woff,woff2,ttf}'])
    .pipe(gulp.dest('./fonts'));
});

gulp.task('js', function () {
  return gulp
    .src([
      './node_modules/angular/angular.js',
      './node_modules/angular-animate/angular-animate.js',
      './node_modules/angular-aria/angular-aria.js',
      './node_modules/angular-messages/angular-messages.js',
      './node_modules/angular-route/angular-route.js',
      './node_modules/angular-material/angular-material.js'
    ])
    .pipe(gulp.dest('./js'));
});

gulp.task('default', ['css', 'fonts', 'js']);
