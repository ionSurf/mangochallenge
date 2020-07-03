import React from 'react';
import PropTypes from "prop-types";
import { Field, reduxForm } from 'redux-form';

class PortraitForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      fields: {
        title : '',
        description : '',
        file : {
          filename : '',
          size : '',
          type : ''
        }
      },
      errors: {},
      loading: false,
      progress: 0,
    }
  }

  static propTypes = {
    previewLogoUrl: PropTypes.string,
    mimeType: PropTypes.string,
    maxWeight: PropTypes.number,
    maxWidth: PropTypes.number,
    maxHeight: PropTypes.number,
    // redux-form porps
    handleSubmit: PropTypes.func.isRequired
  };
  
  static defaultProps = {
    previewLogoUrl: "https://via.placeholder.com/300.png/09f/fff",
    mimeType: "image/jpeg, image/png",
    maxWeight: 2000,
    maxWidth: 100,
    maxHeight: 100
  };

  validateImageWeight = imageFile => {
    if (imageFile && imageFile.size) {
      // Get image size in kilobytes
      const imageFileKb = imageFile.size / 1024;
      const { maxWeight } = this.props;

      if (imageFileKb > maxWeight) {
        return `Image size must be less or equal to ${maxWeight}kb`;
      }
    }
  };
  validateImageFormat = imageFile => {
    if (imageFile) {
      const { mimeType } = this.props;

      if (!mimeType.includes(imageFile.type)) {
        return `Image mime type must be ${mimeType}`;
      }
    }
  };

  renderError({ error, touched }) {
    if (touched && error) {
      return (
        <div className="ui error message">
          <div className="header">{error}</div>
        </div>
      );
    }
    //console.log('test');
  }

  renderInput = ({ input, label, meta }) => {
    const className = `field ${meta.error && meta.touched ? 'error' : ''}`;
    return (
      <div className={className}>
        <label>{label}</label>
        <input {...input} autoComplete="off" />
        {this.renderError(meta)}
      </div>
    );
  };

  renderFileInput = ({ input, type, label, meta }) => {
    const className = `field ${meta.error && meta.touched ? 'error' : ''}`;
    const { mimeType } = this.props;
    //console.log('test');
    return (
      <div className={className}>
        <label>{label}</label>
        <input
          name={input.name}
          type={type}
          accept={mimeType}
          onChange={event => this.onFileInputChange(event, input)}
        />
        {this.renderError(meta)}
      </div>
    );
  };

  onSubmit = formValues => {
    if ( formValues.image !== undefined && formValues.url !== undefined ) 
      delete formValues.url;
    //console.log(formValues)
    this.props.onSubmit(formValues);
  };

  onFilePreview = imageUrl => {
    const previewImageDom = document.querySelector(".preview-image");
    previewImageDom.src = imageUrl;
  };

  onFileInputChange = (event, input) => {
    event.preventDefault();
    let imageFile = event.target.files[0];
    if (imageFile) {
      const localImageUrl = URL.createObjectURL(imageFile);
      const imageObject = new window.Image();

      imageObject.onload = () => {
        imageFile.width = imageObject.naturalWidth;
        imageFile.height = imageObject.naturalHeight;
        input.onChange(imageFile);
        URL.revokeObjectURL(imageFile);
      };
      imageObject.src = localImageUrl;
      this.onFilePreview(localImageUrl);

      this.setState({
        fields : {
          file : {
            filename  : event.target.files[0].name,
            size      : event.target.files[0].size,
            type      : event.target.files[0].type,
          }
        }
      });
    }
  }

  render() {
    
    return (
      <form
        onSubmit={this.props.handleSubmit(this.onSubmit)}
        className="ui form error"
      >
        <img
          src={(this.props.initialValues !== undefined) ? process.env.REACT_APP_API_URL + this.props.initialValues.url : this.props.previewLogoUrl}
          alt="preview"
          className="preview-image"
          style={{ maxWidth: "99%", minWidth: "98%", objectFit: "cover" }}
        />
        <Field
          name="title" 
          component={this.renderInput} 
          label="Enter Title"
           />
        <Field
          name="description"
          component={this.renderInput}
          label="Enter Description"
        />

        <Field
          name="image"
          type="file"
          label="Upload file"
          validate={[
            this.validateImageWeight,
            //this.validateImageWidth,
            //this.validateImageHeight,
            this.validateImageFormat
          ]}
          component={this.renderFileInput}
                  />
        <button className="ui button red">Cancel</button>
        <button className="ui button primary">Submit</button>
      </form>
    );
  }
}

const validate = formValues => {
  const errors = {};

  if (!formValues.title) {
    errors.title = 'You must enter a title';
  }

  if (!formValues.description) {
    errors.description = 'You must enter a description';
  }
  
  return errors;
};

export default reduxForm({
  form: 'PortraitForm',
  validate
})(PortraitForm);
