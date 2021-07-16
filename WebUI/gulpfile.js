const gulp = require('gulp'),
    rename = require('gulp-rename'),
    sass = require('gulp-sass')(require('sass')),
    postcss = require('gulp-postcss'),
    cssnano = require('cssnano'),
    autoprefixer = require('autoprefixer');

var paths = {
    styles: './Assets/Styles/**/*.scss',
    css: './wwwroot/css'
};

gulp.task('styles', function () {
    var plugins = [
        autoprefixer(),
        cssnano()
    ];
    return gulp.src(paths.styles)
        .pipe(sass())

        .pipe(postcss(plugins))

        .pipe(rename(function (path) {
            path.basename = path.basename.toLowerCase() + '.min'
        }))

        .pipe(gulp.dest(paths.css));
});

gulp.task('watch', function () {
    gulp.watch(paths.styles, gulp.series('styles'));
});