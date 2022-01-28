const gulp = require('gulp'),
    rename = require('gulp-rename'),
    sass = require('gulp-sass')(require('node-sass')),
    postcss = require('gulp-postcss'),
    cssnano = require('cssnano'),
    autoprefixer = require('autoprefixer'),
    mergestream = require('merge-stream'),
    clean = require('gulp-clean');

var paths = {
    styles: './Assets/Styles/**/*.scss',
    css: './wwwroot/css',
    libs: './wwwroot/lib',
    webfonts: './wwwroot/webfonts',
    nodeModules: './node_modules'
};

const libraries = [
    { from: 'admin-lte/dist', to: 'admin-lte' },
    { from: 'animate.css', to: 'animate-css', glob: '/**/*.min.js' },
    { from: 'animate.css', to: 'animate-css', glob: '/**/*.min.css' },
    { from: 'bootstrap/dist', to: 'bootstrap' },
    { from: 'chart.js/dist', to: 'chart-js'},
    { from: 'dragula/dist', to: 'dragula' },
    { from: 'dropzone/dist/min', to: 'dropzone'},
    { from: '@fortawesome/fontawesome-free/webfonts', to: '', path: paths.webfonts },
    { from: '@fonticonpicker/fonticonpicker/dist', to: 'fonticonpicker' },
    { from: 'select2/dist', to: 'select2' },
    { from: '@ttskch/select2-bootstrap4-theme/dist/', to: 'select2-bootstrap4-theme' },
    { from: 'bootstrap-select/dist', to: 'bootstrap-select' },
    { from: 'icheck-bootstrap', to: 'icheck-bootstrap', glob: '/**/*.min.css'},
    { from: 'jquery/dist', to: 'jQuery' },
    { from: 'jquery-ui-dist', to: 'jquery-ui', glob: '/**/*.min.js' },
    { from: 'jquery-ui-dist', to: 'jquery-ui', glob: '/**/*.min.css' },
    { from: 'quill/dist', to: 'quill' },
    { from: 'sweetalert2/dist', to: 'sweetalert2' }
];

gulp.task('moveLibs', function () {
    var tasks = [];
    for (const library of libraries) {
        var glob = library.glob ?? '/**/*';
        var path = library.path ?? paths.libs;
        tasks.push(
            gulp.src(paths.nodeModules + '/' + library.from + glob)
                .pipe(gulp.dest(path + '/' + library.to))
        );
    };
    return mergestream(tasks);
});

gulp.task('buildStyles', function () {
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

gulp.task('copyIcon', function () {
    return gulp.src('./Assets/favicon.ico').pipe(gulp.dest('./wwwroot'));
})

gulp.task('cleanLibs', function () {
    return gulp.src(paths.libs, { read: false, allowEmpty: true }).pipe(clean());
});

gulp.task('cleanStyles', function () {
    return gulp.src(paths.css, { read: false, allowEmpty: true }).pipe(clean());
});

gulp.task('cleanFonts', function () {
    return gulp.src(paths.webfonts, { read: false, allowEmpty: true }).pipe(clean());
});

gulp.task('clean', gulp.series('cleanLibs', 'cleanStyles', 'cleanFonts'));

gulp.task('libs', gulp.series('cleanLibs', 'moveLibs'));

gulp.task('styles', gulp.series('cleanStyles', 'buildStyles'));

gulp.task('icon', gulp.series('copyIcon'));

gulp.task('build', gulp.series('libs', 'styles', 'icon'));

gulp.task('watch', function () {
    gulp.watch(paths.styles, gulp.series('styles'));
});