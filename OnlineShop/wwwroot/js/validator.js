// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function Validator(formSelector, options = {}) {
    function getParents(element, selector) {
        while (element.parentElement) {
            if (element.parentElement.matches(selector)) {
                return element.parentElement
            }
            element = element.parentElement
        }
    }

    var formRules = {};

    // có dữ liệu -> return undefine, không có --> return errorMessage
    var validatorRules = {
        required: function (value) {
            return value ? undefined : 'Vui lòng nhập trường này.'
        },
        email: function (value) {
            var regex = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/g;
            return regex.test(value) ? undefined : 'Vui lòng nhập đúng định dạng email.'
        },
        min: function (min) {
            return function (value) {
                return value.length >= min ? undefined : `Vui lòng nhập tối thiểu ${min} ký tự.`
            }
        },
        max: function (max) {
            return function (value) {
                return value.length <= max ? undefined : `Vui lòng nhập tối đa ${max} ký tự.`
            }
        },
    }



    var formElement = document.querySelector(formSelector)
    if (formElement) {
        var inputs = formElement.querySelectorAll('[name][rule]')

        for (var input of inputs) {
            var isRuleHasValue = rule.includes(':')
            var ruleInfo
            var rules = input.getAttribute('rules').split('|')
            for (var rule of rules) {
                if (isRuleHasValue) {
                    var ruleInfo = rule.split(':')
                    rule = ruleInfo[0]
                }
                var ruleFunction = validatorRules[rule]

                if (isRuleHasValue) {
                    ruleFunction = validatorRules[rule](ruleInfo[1])
                }
                if (Array.isArray(formRules[input.name])) {
                    formRules[input.name].push(ruleFunction)
                } else {
                    formRules[input.name] = [ruleFunction]
                }
            }
            // onchange, onblur, oninput
            input.onblur = handleValidate
            input.oninput = handleClearEror

        }


        // handle functions
        function handleValidate(e) {
            var rules = formRules[e.target.name]
            var errorMessage;
            rules.find(function (rule) {
                errorMessage = rule(e.target.value)
                return errorMessage
            })

            // nếu có lỗi, hiển thị ra UI
            if (errorMessage) {
                var formGroup = getParents(e.target, '.form-group')
                if (formGroup) {
                    formGroup.classList.add('invalid')
                    var formMessage = formGroup.querySelector('.form-message')
                    if (formMessage) {
                        formMessage.innerText = errorMessage
                    }
                }
            }
            return !errorMessage
        }
        function handleClearEror(e) {
            var formGroup = getParents(e.target, '.form-group')
            if (formGroup.classList.contains('invalid')) {
                formGroup.classList.remove('invalid')
            }
            var formMessage = formGroup.querySelector('.form-message')
            if (formMessage) {
                formMessage.innerText = ''
            }
        }
    }
    formElement.onsubmit = function (e) {
        e.preventDefault()
        var inputs = formElement.querySelectorAll('[name][rule]')
        var isValid = true 
        for (var input of inputs) {
            if (!handleValidate({ target: input })) {
                isValid = false 
            }
        }
        if (isValid) {
            
            if (typeof options.onSubmit === 'function') {
                return options.onSubmit()
            }
            formElement.submit()
        }
    }
}