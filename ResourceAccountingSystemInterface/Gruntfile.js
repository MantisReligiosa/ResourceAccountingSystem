/// <binding BeforeBuild='all' Clean='clean' ProjectOpened='watch' />

module.exports = function (grunt) {
    require('load-grunt-tasks')(grunt);
    grunt.initConfig({
        clean: ["temp/"],
        copy: {
            font_awesome: {
                files: [
                    {
                        expand: true,
                        cwd: 'node_modules/font-awesome/fonts',
                        src: '**',
                        dest: 'fonts/'
                    }
                ]
            },
            bootstrap_fonts: {
                files: [
                    {
                        expand: true,
                        cwd: 'node_modules/bootstrap/fonts',
                        src: '**',
                        dest: 'fonts/'
                    }
                ]
            }
        },
        concat: {
            options: {
                stripBanners: {
                    block: true,
                    line: true
                }
            },
            vendor: {
                src: [
                    'node_modules/jquery/dist/jquery.js',
                    'node_modules/jquery-ajax-unobtrusive/jquery.unobtrusive-ajax.js',
                    'node_modules/jquery-validation/dist/jquery.validate.js',
                    'node_modules/jquery-validation/dist/localization/messages_ru.js',
                    'node_modules/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js',
                    'node_modules/moment/moment.js',
                    'node_modules/moment/locale/ru.js',
                    'node_modules/bootstrap/dist/js/bootstrap.js',
                    'node_modules/bootstrap/js/collapse.js',
                    'node_modules/bootstrap-multiselect/dist/js/bootstrap-multiselect.js',
                    'node_modules/eonasdan-bootstrap-datetimepicker/build/js/bootstrap-datetimepicker.min.js',
                    'node_modules/respond.js/dest/respond.src.js'
                ],
                dest: 'temp/vendor.js'
            },
            styles_css:
            {
                options: {
                    specialComments: 0
                },
                src: [
                    'Content/Site.css',
                    'node_modules/bootstrap/dist/css/bootstrap.css',
                    'node_modules/bootstrap/dist/css/bootstrap-theme.css',
                    'node_modules/eonasdan-bootstrap-datetimepicker/build/css/bootstrap-datetimepicker.css',
                    'node_modules/bootstrap-multiselect/dist/css/bootstrap-multiselect.css',
                    'node_modules/font-awesome/css/font-awesome.css'
                ],
                dest: 'temp/styles.css'
            },
        },
        uglify: {
            vendor: {
                src: ['temp/vendor.js'],
                dest: 'scripts/vendor.min.js'
            },
        },
        cssmin: {
            styles: {
                src: 'temp/styles.css',
                dest: "css/styles.min.css"
            },
        },
        watch: {
            files: ["Scripts/*.js","!Scripts/*.min.js"],
            tasks: ["jshint"]
        }
    });

    grunt.registerTask("all", ['clean', 'copy', 'concat', 'uglify', 'cssmin']);
};