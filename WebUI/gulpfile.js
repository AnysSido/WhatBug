const gulp = require('gulp'),
    rename = require('gulp-rename'),
    sass = require('gulp-sass')(require('sass')),
    postcss = require('gulp-postcss'),
    cssnano = require('cssnano'),
    autoprefixer = require('autoprefixer'),
    mergestream = require('merge-stream'),
    clean = require('gulp-clean');

var paths = {
    styles: './Assets/Styles/**/*.scss',
    css: './wwwroot/css',
    libs: './wwwroot/lib',
    nodeModules: './node_modules'
};

const libraries = [
    // AdminLTE
    { from: 'admin-lte/dist', to: 'admin-lte' },
    // Animate.css
    { from: 'animate.css', to: 'animate-css', glob: '/**/*.min.js' },
    { from: 'animate.css', to: 'animate-css', glob: '/**/*.min.css' },
    // Bootstrap
    { from: 'bootstrap/dist', to: 'bootstrap' },
    { from: 'dragula/dist', to: 'dragula' },
    // FontAwesome
    { from: '@fortawesome/fontawesome-free/css', to: 'fontawesome-free/css' },
    { from: '@fortawesome/fontawesome-free/webfonts', to: 'fontawesome-free/webfonts' },
    // FontIconPicker
    { from: '@fonticonpicker/fonticonpicker/dist', to: 'fonticonpicker' },
    // Select2
    { from: 'select2/dist', to: 'select2' },
    { from: '@ttskch/select2-bootstrap4-theme/dist/', to: 'select2-bootstrap4-theme' },
    // Bootstrap Select
    { from: 'bootstrap-select/dist', to: 'bootstrap-select' },
    // Bootstrap4-duallistbox
    { from: 'bootstrap4-duallistbox/dist', to: 'bootstrap4-duallistbox' },
    // Highlight.js
    { from: 'highlight.js', to: 'highlight-js' },
    // jQuery
    { from: 'jquery/dist', to: 'jQuery' },
    // JQuery-ui
    { from: 'jquery-ui-dist', to: 'jquery-ui', glob: '/**/*.min.js' },
    { from: 'jquery-ui-dist', to: 'jquery-ui', glob: '/**/*.min.css' },
    // Quill
    { from: 'quill/dist', to: 'quill' },
    // SweetAlert2
    { from: 'sweetalert2/dist', to: 'sweetalert2' }
];

gulp.task('moveLibs', function () {
    var tasks = [];
    for (const library of libraries) {
        var glob = library.glob ?? '/**/*';
        tasks.push(
            gulp.src(paths.nodeModules + '/' + library.from + glob)
                .pipe(gulp.dest(paths.libs + '/' + library.to))
        );
    };
    return mergestream(tasks);
});

gulp.task('cleanLibs', function () {
    return gulp.src(paths.libs, { read: false, allowEmpty: true }).pipe(clean());
});

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

gulp.task('libs', gulp.series('cleanLibs', 'moveLibs'));

gulp.task('watch', function () {
    gulp.watch(paths.styles, gulp.series('styles'));
});