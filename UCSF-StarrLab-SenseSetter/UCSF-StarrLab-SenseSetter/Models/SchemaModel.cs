using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCSF_StarrLab_SenseSetter.Models
{
    class SchemaModel
    {
        private string senseSchema = @"{
                      'type': 'object',
                      'required': [],
                      'properties': {
                        'eventType': {
                          'type': 'object',
                          'required': [],
                          'properties': {
                            'comment': {
                              'type': 'string'
                            },
                            'type': {
                              'type': 'string'
                            }
                          }
                        },
                        'Mode': {
                          'type': 'number'
                        },
                        'Ratio': {
                          'type': 'number'
                        },
                        'SenseOptions': {
                          'type': 'object',
                          'required': [],
                          'properties': {
                            'comment': {
                              'type': 'string'
                            },
                            'TimeDomain': {
                              'type': 'boolean'
                            },
                            'FFT': {
                              'type': 'boolean'
                            },
                            'Power': {
                              'type': 'boolean'
                            },
                            'LD0': {
                              'type': 'boolean'
                            },
                            'LD1': {
                              'type': 'boolean'
                            },
                            'AdaptiveState': {
                              'type': 'boolean'
                            },
                            'LoopRecording': {
                              'type': 'boolean'
                            },
                            'Unused': {
                              'type': 'boolean'
                            }
                          }
                        },
                        'StreamEnables': {
                          'type': 'object',
                          'required': [],
                          'properties': {
                            'comment': {
                              'type': 'string'
                            },
                            'TimeDomain': {
                              'type': 'boolean'
                            },
                            'FFT': {
                              'type': 'boolean'
                            },
                            'Power': {
                              'type': 'boolean'
                            },
                            'Accelerometry': {
                              'type': 'boolean'
                            },
                            'AdaptiveTherapy': {
                              'type': 'boolean'
                            },
                            'AdaptiveState': {
                              'type': 'boolean'
                            },
                            'EventMarker': {
                              'type': 'boolean'
                            },
                            'TimeStamp': {
                              'type': 'boolean'
                            }
                          }
                        },
                        'Sense': {
                          'type': 'object',
                          'required': [],
                          'properties': {
                            'commentTDChannelDefinitions': {
                              'type': 'string'
                            },
                            'commentFilters': {
                              'type': 'string'
                            },
                            'TDSampleRate': {
                              'type': 'number'
                            },
                            'TimeDomains': {
                              'type': 'array',
                              'items': {
                                'type': 'object',
                                'required': [],
                                'properties': {
                                  'ch0': {
                                    'type': 'string'
                                  },
                                  'IsEnabled': {
                                    'type': 'boolean'
                                  },
                                  'Hpf': {
                                    'type': 'number'
                                  },
                                  'Lpf1': {
                                    'type': 'number'
                                  },
                                  'Lpf2': {
                                    'type': 'number'
                                  },
                                  'Inputs': {
                                    'type': 'array',
                                    'items': {
                                      'type': 'number'
                                    }
                                  }
                                }
                              }
                            },
                            'FFT': {
                              'type': 'object',
                              'required': [],
                              'properties': {
                                'commentFFTParameters': {
                                  'type': 'string'
                                },
                                'Channel': {
                                  'type': 'number'
                                },
                                'FftSize': {
                                  'type': 'number'
                                },
                                'FftInterval': {
                                  'type': 'number'
                                },
                                'WindowLoad': {
                                  'type': 'number'
                                },
                                'StreamSizeBins': {
                                  'type': 'number'
                                },
                                'StreamOffsetBins': {
                                  'type': 'number'
                                },
                                'WindowEnabled': {
                                  'type': 'boolean'
                                }
                              }
                            },
                            'Power': {
                              'type': 'array',
                              'items': {
                                'type': 'object',
                                'required': [],
                                'properties': {
                                  'comment': {
                                    'type': 'string'
                                  },
                                  'ChannelPowerBand': {
                                    'type': 'array',
                                    'items': {
                                      'type': 'number'
                                    }
                                  },
                                  'IsEnabled': {
                                    'type': 'boolean'
                                  }
                                }
                              }
                            },
                            'Accelerometer': {
                              'type': 'object',
                              'required': [],
                              'properties': {
                                'commentAcc': {
                                  'type': 'string'
                                },
                                'SampleRateDisabled': {
                                  'type': 'boolean'
                                },
                                'SampleRate': {
                                  'type': 'number'
                                }
                              }
                            },
                              'Misc' : {
                                'type': 'object',
                              'required': [],
                              'properties': {
                                'commentMiscParameters': {
                                  'type': 'string'
                                },
                                  'StreamingRate': {
                                    'type': 'number'
                                  },
                                    'LoopRecordingTriggersState':{
                                      'type': 'number'
                                    },
                                'LoopRecordingTriggersIsEnabled':{
                                  'type': 'boolean'
                                },
                                  'LoopRecordingPostBufferTime' :{
                                    'type': 'number'
                                  },
                                    'Bridging':{
                                      'type': 'number'
                                    }
                              }
                              }
                          }
                        }
                      }
                    }";

        /// <summary>
        /// Gets the Schema for Sense
        /// </summary>
        /// <returns>string that correlates to the sense schema</returns>
        public string GetSenseSchema()
        {
            return senseSchema;
        }
    }
}
